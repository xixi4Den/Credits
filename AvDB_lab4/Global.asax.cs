using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AvDB_lab4.Infrastructure;

namespace AvDB_lab4
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }
    }
}
