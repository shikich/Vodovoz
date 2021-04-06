using System.Web.Http;

namespace DriversAPI
{
    public static class WebApiConfig
    {
        /// <summary>
        /// Настройка роутов для WebAPI и сервисов
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
