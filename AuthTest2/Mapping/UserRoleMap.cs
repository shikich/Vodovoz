using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vodovoz.Identity;

namespace AuthTest2.Mapping
{
    public class UserRoleMap : ClassMap<UserRole>
    {
        public UserRoleMap()
        {
            Table("aspnet_user_roles");

            Id(x => x.Id).Column("id").GeneratedBy.Native();

            Map(x => x.Name).Column("Name");
        }
    }
}