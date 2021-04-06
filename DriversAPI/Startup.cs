using Autofac;
using Microsoft.Owin;
using Owin;
using QS.DomainModel.UoW;
using NHibernate;
using WebAPI.Library.DataAccess;
using Vodovoz.EntityRepositories.Logistic;

[assembly: OwinStartupAttribute(typeof(DriversAPI.Startup))]
namespace DriversAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = AutoFacConfig.RegisterServices(new ContainerBuilder());
            NhibernateConfig.CreateBaseConfig();
            app.UseAutofacMiddleware(container);
            ConfigureAuth(app);
        }
    }
}
