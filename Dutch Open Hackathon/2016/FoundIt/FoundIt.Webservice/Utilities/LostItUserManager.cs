//
// LostItUserManager.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using FoundIt.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace FoundIt.Webservice.Utilities
{
    public class LostItUserStore : UserStore<User>, IUserStore<User>
    {
        public LostItUserStore(LostItContext context) : base(context) { }
    }

    public class LostItUserManager : UserManager<User>
    {
        private LostItUserStore store;

        public LostItUserManager(LostItUserStore store) : base(store)
        {
            this.store = store;
        }

        public static LostItUserManager Create(IdentityFactoryOptions<LostItUserManager> options, IOwinContext context)
        {
            var manager = new LostItUserManager(new LostItUserStore(context.Get<LostItContext>()));
            manager.UserValidator = new UserValidator<User>(manager)
            {
                RequireUniqueEmail = true
            };

            return manager;
        }
    }
}

