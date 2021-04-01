using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NHibernate;

namespace Vodovoz.Identity
{
    //public class CustomUserRole : IdentityUserRole<int> { }
    // public class CustomUserClaim : IdentityUserClaim<int> { }

    //public class CustomRole : IdentityRole<int, CustomUserRole>
    //{

    //}

    public class IdentityUser : IUser<int>
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual ICollection<UserLogin> Logins { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
    }

}
