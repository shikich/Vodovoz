using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using System.Threading.Tasks;

namespace Vodovoz.Identity
{
    public class SignInManager : SignInManager<IdentityUser, int>
    {
        public SignInManager(UserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(userName);

                if (user != null)
                {
                    if (await UserManager.CheckPasswordAsync(user, password))
                    {
                        return SignInStatus.Success;
                    }
                }
            }
            catch (Exception e)
            {
                return SignInStatus.Failure;
            }

            return SignInStatus.Failure;
        }

        public static SignInManager Create(IdentityFactoryOptions<SignInManager> options, IOwinContext context)
        {
            return new SignInManager(context.GetUserManager<UserManager>(), context.Authentication);
        }
    }
}