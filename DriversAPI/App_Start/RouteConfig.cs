using System.Web.Mvc;
using System.Web.Routing;

namespace DriversAPI
{
    public class RouteConfig
    {
        /// <summary>
        /// Регистрация роутинга для MVC
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
