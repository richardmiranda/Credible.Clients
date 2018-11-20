using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using Credible.Clients.Filter;
using Credible.Clients.Models;
using Unity.Attributes;

namespace Credible.Clients.Controllers
{
    [UowActionFilter]
    public class CourseController : Controller
    {
        [Dependency]
        public ICoursePortalService CoursePortalService { get; set; }

        [Dependency]
        public IRegistrationService RegistrationService { get; set; }

        [Dependency]
        public IUserService UserService { get; set; }

        public ActionResult Index()
        {
            var vm = new CoursePortalViewModel
            {
                Courses = CoursePortalService.FindAll()
                    .Select( x=> new Course { Portal_Id = x.Portal_Id, Portal_Nm = x.Portal.Portal_Nm,
                        Course_Portal_Nm = x.Course_Portal_Nm, Course_Portal_Id = x.Course_Portal_Id })
                    .OrderBy(x => x.Portal_Nm).ThenBy(x=>x.Course_Portal_Nm).ToList()
            };
            return View(vm);
        }

        public ActionResult Registrations(int id)
        {
            var course = CoursePortalService.GetCourse(id);
            if (course == null)
            {
                throw new KeyNotFoundException("id");
            }

            var vm = new RegistrationViewModel
            {
                Course_Id = id,
                Course_Nm = course.Course_Portal_Nm,
                Registrations = RegistrationService.FindAll().AsQueryable()
                    .Where(x => x.Course_Portal_Id == id)
                    .Select(x=> new RegistrationModel { User_Id = x.User_Id, First_Nm = x.User.First_Nm, Last_Nm = x.User.Last_Nm, Registration_Dttm = x.Registration_Dttm })
                    .ToList()
            };
            return View(vm);
        }

        public ActionResult Filter(int id, DateTime? startDate, DateTime? endDate)
        {
            var course = CoursePortalService.GetCourse(id);
            if (course == null)
            {
                throw new KeyNotFoundException("id");
            }

            var regs = RegistrationService.FindAll().AsQueryable().Where(x => x.Course_Portal_Id == id);
            if (startDate.HasValue)
            {
                regs = regs.Where(x => x.Registration_Dttm >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                regs = regs.Where(x => x.Registration_Dttm <= endDate.Value);
            }
            var vm = new RegistrationViewModel
            {
                Course_Id = id,
                Course_Nm = course.Course_Portal_Nm,
                Registrations = regs.Select(x => new RegistrationModel { User_Id = x.User_Id, First_Nm = x.User.First_Nm, Last_Nm = x.User.Last_Nm, Registration_Dttm = x.Registration_Dttm })
                    .ToList()
            };
            return PartialView("_FilterPartial", vm);
        }
    }    
   
}