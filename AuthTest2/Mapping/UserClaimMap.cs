using AuthTest2.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vodovoz.Identity;

namespace AuthTest2.Mapping
{
    public class UserClaimMap : ClassMap<UserClaim>
    {
        public UserClaimMap()
        {
            Table("aspnet_user_claims");

            Id(x => x.Id).Column("id").GeneratedBy.Native();

            Map(x => x.ClaimType).Column("claim_type");
            Map(x => x.ClaimValue).Column("claim_value");
        }
    }
}