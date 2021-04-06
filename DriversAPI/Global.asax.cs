using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DriversAPI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Запуск приложения
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Хак для xsp4, удаляет лишний заголовок Content-Length
        /// При необходимости или нахождении другого решения - удалить
        /// </summary>
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            Response.Headers.Remove("Content-Length");
        }
    }
}
