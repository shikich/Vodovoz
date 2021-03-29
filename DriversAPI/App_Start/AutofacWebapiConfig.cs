using Autofac;
using Autofac.Integration.WebApi;
using QS.DomainModel.UoW;
using QS.Project.Services;
using QS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;  
using System.Web;
using System.Web.Http;
using Vodovoz.Core.DataService;
using Vodovoz.EntityRepositories.Logistic;
using Vodovoz.EntityRepositories.Orders;
using WebAPI.Library.DataAccess;
using WebAPI.Library.Models;

namespace DriversAPI.App_Start
{
    public class AutofacWebapiConfig
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

        private static IContainer RegisterServices(ContainerBuilder builder)
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

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}