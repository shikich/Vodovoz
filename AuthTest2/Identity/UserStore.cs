﻿using NHibernate;
using QS.DomainModel.UoW;
using NHibernate.AspNet.Identity;
using System.Threading.Tasks;

namespace AuthTest2.Identity
{
    public class UserStore<TUser> : NHibernate.AspNet.Identity.UserStore<TUser> where TUser : IdentityUser
    {
        public UserStore(IUnitOfWork unitOfWork): base(unitOfWork.Session)
        {
        }

        public UserStore(ISession context) : base(context)
        {
        }

        //public override Task<TUser> FindByNameAsync(string userName)
        //{
        //    return base.FindByNameAsync(userName);
        //}
    }
}