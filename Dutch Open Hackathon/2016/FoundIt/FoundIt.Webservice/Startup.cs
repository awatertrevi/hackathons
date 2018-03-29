//
// Startup.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Web.Http;
using FoundIt.Models;
using FoundIt.Webservice.Providers;
using FoundIt.Webservice.Utilities;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Swashbuckle.Application;
using System.Web.Http.ExceptionHandling;
using FoundIt.Webservice.Exceptions;

namespace FoundIt.Webservice
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.Services.Add(typeof(IExceptionLogger), new AiExceptionLogger());

            config.MapHttpAttributeRoutes();
            config.EnableSwagger(c => c.SingleApiVersion("v1", "FoundIt API")).EnableSwaggerUi(c => c.InjectJavaScript(GetType().Assembly, "LostIt.Scripts.bearer.js"));

            app.CreatePerOwinContext(LostItContext.Create);
            app.CreatePerOwinContext<LostItUserManager>(LostItUserManager.Create);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token/"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(182.5), // Half a year.
                Provider = new LostItTokenProvider()
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }

    }
}