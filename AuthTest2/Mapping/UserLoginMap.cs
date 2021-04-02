using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vodovoz.Identity;

namespace AuthTest2.Mapping
{
    public class UserLoginMap : ClassMap<UserLogin>
    {
        public UserLoginMap()
        {
            Table("aspnet_user_logins");

            Id(x => x.Id).Column("id").GeneratedBy.Native();

            Map(x => x.LoginProvider).Column("login_provider");
            Map(x => x.ProviderKey).Column("provider_key");

            References(x => x.User).Column("user_id");
        }
    }
}