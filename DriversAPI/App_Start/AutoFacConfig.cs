using Autofac;
using Autofac.Integration.WebApi;
using DriversAPI.Controllers;
using QS.DomainModel.UoW;
using QS.Project.Services;
using QS.Services;
using System.Reflection;
using System.Web.Http;
using Vodovoz.Core.DataService;
using Vodovoz.EntityRepositories.Logistic;
using Vodovoz.EntityRepositories.Orders;
using WebAPI.Library.DataAccess;
using WebAPI.Library.Models;

namespace DriversAPI
{
    public class AutoFacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration configuration)
        {
            Initialize(configuration, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public static IContainer RegisterServices(ContainerBuilder containerBuilder)
        {
            //Dependencies

            #region База
            containerBuilder.Register(c => UnitOfWorkFactory.GetDefaultFactory).As<IUnitOfWorkFactory>().InstancePerRequest();
            containerBuilder.Register(c => UnitOfWorkFactory.CreateWithoutRoot()).As<IUnitOfWork>().InstancePerRequest();
            containerBuilder.RegisterType<BaseParametersProvider>().AsSelf().InstancePerRequest();
            #endregion

            #region Сервисы
            containerBuilder.Register(c => ServicesConfig.CommonServices).As<ICommonServices>().InstancePerRequest();
            containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            #endregion

            #region Repository
            containerBuilder.RegisterType<RouteListRepository>().As<IRouteListRepository>().InstancePerRequest();
            containerBuilder.RegisterType<OrderSingletonRepository>().As<IOrderRepository>().InstancePerRequest();
            #endregion

            #region API.Library
            containerBuilder.RegisterType<APIRouteListData>().AsSelf().InstancePerRequest();
            containerBuilder.RegisterType<APIRouteList>().AsSelf().InstancePerRequest();
            containerBuilder.RegisterType<APIRouteListAddress>().AsSelf().InstancePerRequest();
            containerBuilder.RegisterType<APIAddress>().AsSelf().InstancePerRequest();
            #endregion

            //Register your Web API controllers.  
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();

            // Конфигурация подключения к БД, менеджера пользователей и менеджера аутентификации
            // для использования одного экземпляра на запрос
            containerBuilder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            containerBuilder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = containerBuilder.Build();

            return Container;
        }
    }
}
