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
    public class ClientController : Controller
    {
        [Dependency]
        public IPortalService PortalService { get; set; }
        [Dependency]
        public ICoursePortalService CoursePortalService { get; set; }
        public ActionResult Index()
        {
            var portals = PortalService.FindAll();
            var vm = new PortalViewModel();
            if (portals != null)
            {
                vm.Portals = portals.ToList();
                vm.Total = portals.Count();
            };
            return View(vm);
        }

        public ActionResult Detail(int id)
        {
            var vm = new DetailViewModel
            {
                Portal = PortalService.GetPortal(id),
                Courses = CoursePortalService.FindAll().Where(x=>x.Portal_Id == id).ToList()
            };
            return View(vm);
        }
    }
}