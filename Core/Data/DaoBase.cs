using System;

namespace Core.Data
{
    public class DaoBase
    {
        private Func<IUnitOfWork> _unitOfWorkFactory;

        public DaoBase()
        {
        }

        public DaoBase(Func<IUnitOfWork> unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null) throw new ArgumentNullException("unitOfWorkFactory");

            this._unitOfWorkFactory = unitOfWorkFactory;
        }

        private IUnitOfWork _dbContext;
        protected IUnitOfWork DBContext
        {
            get
            {
                if (null == _unitOfWorkFactory)
                {
                    throw new InvalidOperationException("The DAO was not initialized with a unit-of-work factory");
                }
                if (null == _dbContext)
                {
                    this._dbContext = this._unitOfWorkFactory.Invoke();
                }
                return this._dbContext;
            }
        }
    }
}
