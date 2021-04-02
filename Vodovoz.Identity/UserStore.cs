using Microsoft.AspNet.Identity;
using NHibernate;
using QS.DomainModel.UoW;
using System;
using System.Threading.Tasks;

namespace Vodovoz.Identity
{
    public class UserStore 
        : IUserStore<IdentityUser, int>,
        IUserPasswordStore<IdentityUser, int>,
        IUserEmailStore<IdentityUser, int>
    {
        public UserStore(IUnitOfWork unitOfWork) //: base(unitOfWork.Session)
        {
            UoW = unitOfWork;
        }

        public UserStore(ISession context) //: base(context)
        {
        }

        public IUnitOfWork UoW { get; private set; }

        public async Task CreateAsync(IdentityUser user)
        {
            await UoW.Session.SaveAsync(user);
        }

        public Task DeleteAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityUser> FindByNameAsync(string userName)
        {
            return await UoW.Session.QueryOver<IdentityUser>().Where(x => x.UserName == userName).SingleOrDefaultAsync();
        }

        public async Task<string> GetEmailAsync(IdentityUser user)
        {
            return "";
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public async Task SetEmailAsync(IdentityUser user, string email)
        {
            
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public async Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            await UpdateAsync(user);
        }

        public async Task UpdateAsync(IdentityUser user)
        {
            await UoW.Session.SaveOrUpdateAsync(user);
        }
    }
}