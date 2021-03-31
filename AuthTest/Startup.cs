using AuthTest.App_Start;
using AuthTest.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using NHibernate;
using Owin;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Owin.Security.Jwt;
using Microsoft.IdentityModel.Tokens;
using Autofac;

[assembly: OwinStartup(typeof(AuthTest.Startup))]

namespace AuthTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var container = AutofacWebapiConfig.RegisterServices(new ContainerBuilder());

            NhibernateConfig.CreateBaseConfig();

            app.UseAutofacMiddleware(container);

            app.CreatePerOwinContext(() =>
                new UserManager(
                    new IdentityStore(
                        DependencyResolver.Current.GetServices<ISession>().FirstOrDefault()
                        )
                    )
                );

            app.CreatePerOwinContext<SignInManager>((options, context) =>
               new SignInManager(context.GetUserManager<UserManager>(), context.Authentication));

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions()
            {
                 TokenValidationParameters = new TokenValidationParameters()
                 {
                      ValidateAudience = false,
                      ValidateIssuer = true,
                      ValidIssuer = "localhost",
                 }
            });
        }

    }
}
