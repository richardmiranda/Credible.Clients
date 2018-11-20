using System.Collections.Generic;
using Core.Models;

namespace Credible.Clients.Models
{
    public class CoursePortalViewModel 
    {
        public IEnumerable<Course> Courses { get; set; }
    }

    public class Course
    {
        public int Portal_Id { get; set; }
        public string Portal_Nm { get; set; }
        public int Course_Portal_Id { get; set; }
        public string Course_Portal_Nm { get; set; }
    }
}