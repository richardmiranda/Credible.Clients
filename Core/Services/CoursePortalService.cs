using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Models;
using Unity.Attributes;

namespace Core.Services
{
    public interface ICoursePortalService
    {
        IEnumerable<CoursePortal> FindAll();
        CoursePortal GetCourse(int id);
    }

    public class CoursePortalService : ICoursePortalService
    {
        [Dependency]
        public ICoursePortalDao CoursePortalDao { get; set; }

        public IEnumerable<CoursePortal> FindAll()
        {
            return CoursePortalDao.FindAll();
        }

        public CoursePortal GetCourse(int id)
        {
            return CoursePortalDao.FindAll().AsQueryable().FirstOrDefault(x => x.Course_Portal_Id == id);
        }

    }
}