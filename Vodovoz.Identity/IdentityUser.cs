using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NHibernate;

namespace Vodovoz.Identity
{
    public class IdentityUser : IUser<int>
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual IList<UserLogin> Logins { get; set; }
        public virtual IList<UserRole> Roles { get; set; }
        public virtual IList<UserClaim> Claims { get; set; }
    }

}
