using AuthTest2.Models;
using FluentNHibernate.Mapping;

namespace AuthTest2.Mapping
{
    public class ApplicationUserMap : ClassMap<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            Table("aspnet_users");

            Id(x => x.Id).Column("id").GeneratedBy.Native();

            Map(x => x.UserName).Column("user_name");

            HasMany(x => x.Claims).Cascade.All().Inverse().KeyColumn("user_id");
            HasMany(x => x.Logins).Cascade.All().Inverse().KeyColumn("user_id");

            HasManyToMany(x => x.Roles).Table("aspnet_user_roles")
                .ParentKeyColumn("user_id")
                .ChildKeyColumn("role_id");
        }
    }
}
