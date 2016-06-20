using System.Web.Mvc;
using System.Web.Routing;

namespace ResourceManagementSystem2
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Home", "", new { controller = "Scheduler", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("Error", "error", new { controller = "Error", action = "Error", id = UrlParameter.Optional }
            );

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Scheduler", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}