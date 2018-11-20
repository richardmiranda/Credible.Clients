using System;
using System.Web;
using System.Web.Mvc;
using Core.Data;

namespace Credible.Clients.Filter
{
    public class UowActionFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (null != HttpContext.Current.Session)
            {
                string currentUser = (String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)
                    ? "notimplemented"
                    : HttpContext.Current.User.Identity.Name);
                HttpContext.Current.Session["CredibleUoW"] = new UnitOfWork(currentUser);
            }
        }


        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (null == HttpContext.Current.Session)
            {
                return;
            }

            var uow = (IUnitOfWork) HttpContext.Current.Session["CredibleUoW"];
            if (null == uow) return;
            if (null == filterContext.Exception)
            {
                try
                {
                    uow.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("Error executing. " + e);
                }
            }

            uow.Dispose();
        }

    }
}