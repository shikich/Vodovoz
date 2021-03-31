using DriversAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DriversAPI.Controllers
{
    public class TestController : ApiController
    {
        private static CustomUserManager _customUserManager;

        public TestController(CustomUserManager customUserManager)
        {
            _customUserManager = customUserManager ?? throw new ArgumentNullException(nameof(customUserManager));
        }

        public async Task<bool> Authenticate(string name, string password)
        {
            if (await _customUserManager.FindAsync(name, password) != null)
            {
                return true;
            }

            return false;
        }
    }
}