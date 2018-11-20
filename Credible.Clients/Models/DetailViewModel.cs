
using System.Collections.Generic;
using Core.Models;

namespace Credible.Clients.Models
{
    public class DetailViewModel
    {
        public Portal Portal { get; set; }
        public IEnumerable<CoursePortal> Courses { get; set; }
    }

}
