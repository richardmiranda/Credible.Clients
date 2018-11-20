using System;
using System.Collections.Generic;
using System.Linq;
using Core.Models;

namespace Core.Data
{
    public interface IPortalDao
    {
        IEnumerable<Portal> FindAll();
    }

    public class PortalDao : DaoBase, IPortalDao
    {
        public PortalDao(Func<IUnitOfWork> uowFactory) : base(uowFactory)
        {
        }

        public IEnumerable<Portal> FindAll()
        {
            return DBContext.Portal.AsQueryable();
        }

    }
}