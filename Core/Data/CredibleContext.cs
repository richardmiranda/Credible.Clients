using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using Core.Models;

namespace Core.Data
{
    public class CredibleContext : DbContext
    {

        public CredibleContext()
            : base("CredibleContainer")
        {
            Configuration.ProxyCreationEnabled = false;

        }

        public DbSet<Portal> Portal { get; set; }
        public DbSet<CoursePortal> CoursePortal { get; set; }
        public DbSet<Registration> Registration { get; set; }
        public DbSet<User> User { get; set; }

        public ObjectContext AsObjectContext()
        {
            var contextAdapter = this as IObjectContextAdapter;
            return contextAdapter.ObjectContext;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public IEnumerable<DbEntityEntry> GetPendingChanges()
        {
            return this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);
        }


    }

    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();

        IEnumerable<T> Query<T>(string query, params object[] parameters);
        int ExecuteSqlCommand(string query, params Object[] parameters);
        /// <summary>
        /// Migrates changes from a detached version of an entity to the current version so that the changes 
        /// can be persisted
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="current">The current version of the entity, retrieved using <seealso cref="Find"/> or some other method</param>
        /// <param name="modified">The detached version of the entity containing the changes that should be persisted</param>
        void UpdateFromDetached<T>(T current, T modified) where T : class;

        /// <summary>
        /// Updates a collection of objects in the context with data from the data source
        /// </summary>
        /// <param name="mode">
        /// A <see cref="RefreshMode"/> value that indicates whether property changes in the object context are overwritten with 
        /// property values from the data source.
        /// </param>
        /// <param name="collection">The collection of objects to refresh</param>
        void Refresh(RefreshMode mode, IEnumerable collection);

        IDbSet<Portal> Portal { get; set; }
        IDbSet<CoursePortal> CoursePortal { get; set; }
        IDbSet<Registration> Registration { get; set; }
        IDbSet<User> User { get; set; }

    }

    public class UnitOfWork : IUnitOfWork
    {

        public const int SqlServerDeadlockErrorNumber = 1205;

        /// <summary>
        /// Called prior to saving changes
        /// </summary>
        public event EventHandler Saving;

        public UnitOfWork() : this("DefaultedUser") { }

        public UnitOfWork(string currentuser)
        {
            _currentUser = currentuser;
        }

        private CredibleContext _dbContext = new CredibleContext();

        /// <summary>
        /// For infrastructure use only.
        /// </summary>
        public CredibleContext Context
        {
            get { return this._dbContext; }
        }

        private string _currentUser = String.Empty;

        private int? _maxDeadlockRetryAttempts;

        public int? MaxDeadlockRetryAttempts
        {
            get
            {
                return _maxDeadlockRetryAttempts;
            }
            set
            {
                _maxDeadlockRetryAttempts = value;
            }
        }

        public int SaveChanges()
        {
            var changedEntities = this._dbContext.GetPendingChanges();
            this.OnSaving();

            var count = this._dbContext.SaveChanges();

            this.Context.SaveChanges();

            return count;

        }

        private void OnSaving()
        {
            var SavingEvent = this.Saving;
            if (null != SavingEvent)
            {
                SavingEvent(this, null);
            }
        }


        private static string BuildEntityKeyForDisplay(EntityKey entityKey)
        {
            if (null == entityKey.EntityKeyValues) return string.Empty;

            var keyValues = entityKey.EntityKeyValues
                .Where(m => null != m)
                .Select(m => m.ToString());
            var key = string.Join("+", keyValues);
            return key;
        }

        public IEnumerable<T> Query<T>(string query, params object[] parameters)
        {
            bool retrying = false;
            int availableAttempts = (this.MaxDeadlockRetryAttempts ?? 0) + 1;
            while (availableAttempts > 0)
            {
                try
                {
                    var parms = parameters;
                    if (retrying)
                    {
                        parms = this.CloneParameters(parameters);
                    }

                    var results = this._dbContext.Database.SqlQuery<T>(query, parameters);

                    if (retrying)
                    {
                        this.MigrateOutputParameters(parms, parameters);
                    }

                    return results;
                }
                catch (SqlException sqlException)
                {
                    if (sqlException.Number == SqlServerDeadlockErrorNumber)
                    {
                        availableAttempts--;
                        if (availableAttempts > 0)
                        {
                            retrying = true;
                            continue;
                        }
                    }

                    throw;

                }
            }

            throw new WarningException("Application flow has reached a point that should be be accessible.");

        }

        public int ExecuteSqlCommand(string sql, params Object[] parameters)
        {
            bool retrying = false;
            int availableAttempts = (this.MaxDeadlockRetryAttempts ?? 0) + 1;
            while (availableAttempts > 0)
            {
                try
                {
                    var parms = parameters;
                    if (retrying)
                    {
                        parms = this.CloneParameters(parameters);
                    }

                    var cnt = this._dbContext.Database.ExecuteSqlCommand(sql, parameters);

                    if (retrying)
                    {
                        this.MigrateOutputParameters(parms, parameters);
                    }

                    return cnt;
                }
                catch (SqlException sqlException)
                {
                    if (sqlException.Number == SqlServerDeadlockErrorNumber)
                    {
                        availableAttempts--;
                        if (availableAttempts > 0)
                        {
                            retrying = true;
                            continue;
                        }
                    }

                    throw;

                }
            }

            throw new WarningException("Application flow has reached a point that should be be accessible.");
        }

        /// <summary>
        /// Clone the parameters within the supplied list.
        /// </summary>
        /// <param name="sourceParameters">The source parameters.</param>
        /// <returns>A new list of parameters where any SqlParameters have been cloned</returns>
        /// <remarks>
        /// A two-way reference is created when a SqlParameter is added to a SqlParameterCollection. The collection
        /// has a reference to the parameters and the parameter has a reference to the collection. This prevents the 
        /// parameter from being added to another SqlParameterCollection and that breaks the deadlock retry funtionality
        /// that wraps the call to ExecuteSqlCommand and Query. SqlParameter implements IClonable. Clone() copies all
        /// the properties of the source parameter to a new instance but does not set the internal Parent property. This
        /// enables the retry functionality to work.
        /// </remarks>
        private object[] CloneParameters(object[] sourceParameters)
        {
            var clones = new List<object>(sourceParameters.Length);
            foreach (var parameter in sourceParameters)
            {
                if (parameter is SqlParameter)
                {
                    clones.Add(((ICloneable)parameter).Clone());
                }
                else
                {
                    clones.Add(parameter);
                }
            }

            return clones.ToArray();
        }

        private void MigrateOutputParameters(object[] source, object[] destination)
        {
            if (source.Length != destination.Length)
            {
                throw new InvalidOperationException("The length of the source and destination arrays must be the same");
            }

            for (int index = 0; index < source.Length; index++)
            {
                var s = source[index];
                var d = destination[index];
                if (object.ReferenceEquals(s, d))
                {
                    continue;
                }

                if (s is DbParameter)
                {
                    var sp = s as DbParameter;
                    var dp = d as DbParameter;

                    if (sp.Direction == ParameterDirection.Output || sp.Direction == ParameterDirection.InputOutput)
                    {
                        dp.Value = sp.Value;
                    }
                }
            }
        }

        /// <summary>
        /// Migrates changes from a detached version of an entity to the current version so that the changes 
        /// can be persisted
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="current">The current version of the entity, retrieved using <seealso cref="Find"/> or some other method</param>
        /// <param name="modified">The detached version of the entity containing the changes that should be persisted</param>
        public void UpdateFromDetached<T>(T current, T modified) where T : class
        {
            this._dbContext.Entry(current).CurrentValues.SetValues(modified);
        }

        /// <summary>
        /// Updates a collection of objects in the context with data from the data source
        /// </summary>
        /// <param name="mode">
        /// A <see cref="RefreshMode"/> value that indicates whether property changes in the object context are overwritten with 
        /// property values from the data source.
        /// </param>
        /// <param name="collection">The collection of objects to refresh</param>
        public void Refresh(RefreshMode mode, IEnumerable collection)
        {
            this._dbContext.AsObjectContext().Refresh(mode, collection);
        }

        public IDbSet<Portal> Portal
        {
            get { return _dbContext.Portal; }
            set { throw new NotImplementedException(); }
        }

        public IDbSet<CoursePortal> CoursePortal
        {
            get { return _dbContext.CoursePortal; }
            set { throw new NotImplementedException(); }
        }

        public IDbSet<Registration> Registration
        {
            get { return _dbContext.Registration; }
            set { throw new NotImplementedException(); }
        }

        public IDbSet<User> User
        {
            get { return _dbContext.User; }
            set { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            this._dbContext.Dispose();
        }
    }


}
