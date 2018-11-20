using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Core.Data;
using Core.Services;
using Unity;
using Unity.Injection;

namespace Credible.Clients
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Database Connection String from Web.Config
        /// </summary>
        public string DataBaseConnectionString = "NOT SET";
        protected void Application_Start()
        {
            SetDbConnectionString();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = InitContainer(DataBaseConnectionString);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        /// <summary>
        /// Set the database connection string from the web.config file
        /// </summary>
        private void SetDbConnectionString()
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["CredibleContainer"].ToString()))
            {
                DataBaseConnectionString = ConfigurationManager.ConnectionStrings["CredibleContainer"].ToString();
            }

            HttpContext.Current.Application.Add("DBConnection", DataBaseConnectionString);
        }

        private static IUnityContainer InitContainer(string dbconnectstring)
        {
            var container = new UnityContainer();

            Func<UnitOfWork> uowFactory = () => (UnitOfWork)HttpContext.Current.Session["CredibleUoW"];

            container.RegisterType<IUserDao, UserDao>(new InjectionConstructor(uowFactory));
            container.RegisterType<IPortalDao, PortalDao>(new InjectionConstructor(uowFactory));
            container.RegisterType<ICoursePortalDao, CoursePortalDao>(new InjectionConstructor(uowFactory));
            container.RegisterType<IRegistrationDao, RegistrationDao>(new InjectionConstructor(uowFactory));

            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IPortalService, PortalService>();
            container.RegisterType<ICoursePortalService, CoursePortalService>();
            container.RegisterType<IRegistrationService, RegistrationService>();
            return container;
        }

        public class UnityDependencyResolver : IDependencyResolver
        {
            IUnityContainer container;
            public UnityDependencyResolver(IUnityContainer container)
            {
                this.container = container;
            }

            public object GetService(Type serviceType)
            {
                try
                {
                    return container.Resolve(serviceType);
                }
                catch (Exception e)
                {
#if DEBUG
                    Debug.WriteLine("Error while resolving type {0}", serviceType);
                    Debug.WriteLine(e.ToString());
#endif
                    return null;
                }
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                try
                {
                    return container.ResolveAll(serviceType);
                }
                catch (Exception e)
                {
#if DEBUG
                    Debug.WriteLine("Error while resolving type {0}", serviceType);
                    Debug.WriteLine(e.ToString());
#endif
                    return new List<object>();
                }
            }
        }
    }
}
