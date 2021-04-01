using FluentNHibernate.Mapping;

namespace AuthTest2.Models
{
    public class ApplicationUserMap : ClassMap<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            Table("aspnet_users");

            Id(x => x.Id).Column("id").GeneratedBy.Native();

            Map(x => x.UserName).Column("user_name");
            Map(x => x.PasswordHash).Column("password_hash");
            Map(x => x.SecurityStamp).Column("security_stamp");
            Map(x => x.AccessFailedCount).Column("access_failed_count");
        }
    }
}
