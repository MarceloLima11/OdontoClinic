using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using Infrastructure.Data.Seed;
using Infrastructure.NHibernate;

namespace Presentation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
            DatabaseSeeder.Popular(NHibernateHelper.OpenSession());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
