using AuthTest2.Models;
using FluentNHibernate.Mapping;

namespace AuthTest2.Mapping
{
    public class UserLoginMap : ClassMap<ApplicationUserLogin>
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