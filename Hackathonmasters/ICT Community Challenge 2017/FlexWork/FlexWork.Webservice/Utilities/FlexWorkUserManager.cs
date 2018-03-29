//
// FitmapAppUserManager.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Runtime.Remoting.Contexts;
using FlexWork.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace FlexWork.Utilities
{
    public class FlexWorkUserStore : UserStore<User>, IUserStore<User>
    {
        public FlexWorkUserStore(FlexWorkContext context) : base(context) { }
    }

    public class FlexWorkUserManager : UserManager<User>
    {
        private FlexWorkUserStore store;

        public FlexWorkUserManager(FlexWorkUserStore store) : base(store)
        {
            this.store = store;
        }

        public static FlexWorkUserManager Create(IdentityFactoryOptions<FlexWorkUserManager> options, IOwinContext context)
        {
            var manager = new FlexWorkUserManager(new FlexWorkUserStore(context.Get<FlexWorkContext>()));
            manager.UserValidator = new UserValidator<User>(manager)
            {
                RequireUniqueEmail = false,
                AllowOnlyAlphanumericUserNames = false
            };

            return manager;
        }
    }
}