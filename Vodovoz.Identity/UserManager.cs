using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vodovoz.Identity
{
    public class UserManager : UserManager<IdentityUser, int>
    {
        public UserManager(IUserStore<IdentityUser, int> store) : base(store)
        {
        }
    }
}
