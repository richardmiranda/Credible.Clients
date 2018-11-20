using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Models;

namespace Core.Data
{
    public interface IRegistrationDao
    {
        IEnumerable<Registration> FindAll();
    }

    public class RegistrationDao : DaoBase, IRegistrationDao
    {
        public RegistrationDao(Func<IUnitOfWork> uowFactory) : base(uowFactory)
        {
        }

        public IEnumerable<Registration> FindAll()
        {
            return DBContext.Registration
                .Include("User")
                .Include("CoursePortal")
                .AsQueryable();
        }

    }
}