using Microsoft.AspNetCore.Identity;

namespace Vodovoz.Identity
{
    public class UserLogin : IdentityUserLogin<int>
    {
        public virtual int Id { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}