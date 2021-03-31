using Autofac;
using Autofac.Integration.WebApi;
using QS.DomainModel.UoW;
using QS.Project.Services;
using QS.Services;
using System.Reflection;
using Vodovoz.Core.DataService;
using Vodovoz.EntityRepositories.Logistic;
using Vodovoz.EntityRepositories.Orders;
using WebAPI.Library.DataAccess;
using WebAPI.Library.Models;

namespace AuthTest.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Dependencies

            #region База
            builder.Register(c => UnitOfWorkFactory.GetDefaultFactory).As<IUnitOfWorkFactory>();
            builder.RegisterType<BaseParametersProvider>().AsSelf();
            #endregion

            #region Сервисы
            builder.Register(c => ServicesConfig.CommonServices).As<ICommonServices>();
            builder.RegisterType<UserService>().As<IUserService>();
            #endregion

            #region Repository
            builder.RegisterType<RouteListRepository>().As<IRouteListRepository>();
            builder.RegisterType<OrderSingletonRepository>().As<IOrderRepository>();
            #endregion

            #region API.Library
            builder.RegisterType<APIRouteListData>().AsSelf();
            builder.RegisterType<APIRouteList>().AsSelf();
            builder.RegisterType<APIRouteListAddress>().AsSelf();
            builder.RegisterType<APIAddress>().AsSelf();
            //builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetAssembly(typeof(APIRouteListData)))
            //    .AsSelf();
            #endregion

            return builder.Build();
        }
    }
}