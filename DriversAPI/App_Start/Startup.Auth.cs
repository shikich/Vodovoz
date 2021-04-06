using AuthTest.App_Start;
using DriversAPI.Models;
using DriversAPI.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using NHibernate;
using Owin;
using QS.DomainModel.UoW;
using System;

namespace DriversAPI
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // Для дополнительной информации о конфигурации аутентификации посетите https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Конфигурация подключения к БД, менеджера пользователей и менеджера аутентификации
            // для использования одного экземпляра на запрос
            NhibernateConfig.CreateBaseConfig();
            app.CreatePerOwinContext<IUnitOfWork>(() => UnitOfWorkFactory.CreateWithoutRoot());
            app.CreatePerOwinContext<ISession>((p,c) => c.Get<IUnitOfWork>().Session);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Позволяет приложения использовать cookie (печеньки) для хранения информации о вошедшем пользователе
            // и использовать cookie для временного хранения информации о пользователе вошедшем через внешние сервисы аутентификации
            // Настройка cookie для аутентификации
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Позволяет приложению валидировать штамп безопасности когда пользователь прошодит аутентификацию
                    // Это возможность безопасности используется при смене пароля или добавлении внешнего логина к аккаунту
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager, DefaultAuthenticationTypes.ApplicationCookie))
                }
            });            

            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                //AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}