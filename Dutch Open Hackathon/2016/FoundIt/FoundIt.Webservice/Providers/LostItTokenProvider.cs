//
// LostItTokenProvider.cs
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
using FoundIt.Models;
using FoundIt.Webservice.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace FoundIt.Webservice.Providers
{
    public class LostItTokenProvider : OAuthAuthorizationServerProvider
    {
        protected readonly LostItContext Database = new LostItContext();

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            if (context.GrantType.ToLower() == "facebook")
            {
                var platform = context.Parameters.Get("platform");

                if (platform != "iOS" && platform != "Android" && platform != "Web")
                    platform = "Unspecified";

                try
                {
                    var fbToken = context.Parameters.Get("access_token");
                    var fbClient = new FacebookClient(fbToken);
                    dynamic response = await fbClient.GetTaskAsync("me", new { fields = "id,first_name,middle_name,last_name,email,gender,birthday,picture.type(large)" });

                    string email = response.email;

                    var userManager = context.OwinContext.GetUserManager<LostItUserManager>();
                    var user = await userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new User()
                        {
                            FacebookId = long.Parse(response.id),
                            FirstName = response.first_name,
                            MiddleName = response.middle_name,
                            LastName = response.last_name,
                            Gender = response.gender,
                            ProfilePhoto = response.picture.data.url,
                            UserName = email,
                            Email = email,
                            EmailConfirmed = true,
                            Platform = platform,
                        };

                        await userManager.CreateAsync(user);
                    }

                    var oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
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