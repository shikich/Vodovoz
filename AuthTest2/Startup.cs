using System;
using System.Collections.Generic;
using System.Linq;
using AuthTest2.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AuthTest2.Startup))]

namespace AuthTest2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            NhibernateConfig.CreateBaseConfig();
            ConfigureAuth(app);
        }
    }
}
