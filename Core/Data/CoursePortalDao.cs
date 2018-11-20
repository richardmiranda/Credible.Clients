using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Models;

namespace Core.Data
{
    public interface ICoursePortalDao
    {
        IEnumerable<CoursePortal> FindAll();
    }

    public class CoursePortalDao : DaoBase, ICoursePortalDao
    {
        public CoursePortalDao(Func<IUnitOfWork> uowFactory) : base(uowFactory)
        {
        }

        public IEnumerable<CoursePortal> FindAll()
        {
            return DBContext.CoursePortal
                .Include("Portal")
                .AsQueryable();
        }

    }
}