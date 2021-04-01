using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vodovoz.Identity
{
    public class UserValidator : UserValidator<IdentityUser, int>
    {
        public UserValidator(UserManager<IdentityUser, int> manager) : base(manager)
        {
        }
    }
}
