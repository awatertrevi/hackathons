//
// FitmapAppTokenProvider.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Facebook;
using FlexWork.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using FlexWork.Utilities;

namespace FlexWork.Providers
{
    public class FlexWorkTokenProvider : OAuthAuthorizationServerProvider
    {
        protected readonly FlexWorkContext Context = new FlexWorkContext();

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            var platform = context.Parameters.Get("platform");

            if (context.GrantType.ToLower() == "facebook")
            {
                try
                {
                    var fbToken = context.Parameters.Get("access_token");
                    var fbClient = new FacebookClient(fbToken);
                    dynamic response = await fbClient.GetTaskAsync("me", new { fields = "id,first_name,middle_name,last_name,email,gender,picture.type(large)" });

                    long facebookId = long.Parse(response.id);

                    var userManager = context.OwinContext.GetUserManager<FlexWorkUserManager>();
                    var userId = Context.Users.SingleOrDefault(u => u.FacebookId == facebookId)?.Id;

                    User user;

                    if (userId == null)
                    {
                        user = new User()
                        {
                            FacebookId = long.Parse(response.id),
                            FirstName = response.first_name,
                            MiddleName = response.middle_name,
                            LastName = response.last_name,
                            Gender = response.gender,
                            ProfilePhoto = response.picture.data.url,
                            UserName = response.email,
                            Email = response.email,
                            EmailConfirmed = true,
                            iOS = platform == "iOS",
                            Android = platform == "Android"
                        };

                        await userManager.CreateAsync(user);
                    }

                    else
                    {
                        user = await userManager.FindByIdAsync(userId);

                        if (user.Android == false)
                            user.Android = platform == "Android";

                        if (user.iOS == false)
                            user.iOS = platform == "iOS";

                        user.FirstName = response.first_name;
                        user.MiddleName = response.middle_name;
                        user.LastName = response.last_name;
                        user.Gender = response.gender;
                        user.ProfilePhoto = response.picture.data.url;
                        user.Email = response.email;
                        user.UserName = response.email;

                        await userManager.UpdateAsync(user);
                    }

                    var oAuthIdentity =
                        await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
                    var ticket = new AuthenticationTicket(oAuthIdentity,
                        new AuthenticationProperties(new Dictionary<string, string>
                        {
                            {"userName", user.UserName}
                        }));

                    context.Validated(ticket);
                }

                catch (FacebookOAuthException e)
                {
                    context.SetError("facebook_grant_error", e.ErrorUserMsg);
                }
            }
        }
    }
}