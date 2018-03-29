//
// Startup.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Web.Http;
using FlexWork.Models;
using FlexWork.Providers;
using FlexWork.Utilities;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Swashbuckle.Application;
using Newtonsoft.Json;

namespace FlexWork
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();
            
			config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            config.MapHttpAttributeRoutes();
			config.EnableSwagger(c => c.SingleApiVersion("v1", "FlexWork API")).EnableSwaggerUi(c => c.InjectJavaScript(GetType().Assembly, "FlexWork.Scripts.bearer.js"));

            app.CreatePerOwinContext(FlexWorkContext.Create);
            app.CreatePerOwinContext<FlexWorkUserManager>(FlexWorkUserManager.Create);

            var now = DateTime.Now;
            var tokenExpireTimeSpan = now.AddYears(3) - now;

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                //#if DEBUG
                AllowInsecureHttp = true,
                //#endif
                TokenEndpointPath = new PathString("/oauth2/token/"),
                AccessTokenExpireTimeSpan = tokenExpireTimeSpan,
                Provider = new FlexWorkTokenProvider()
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
			app.UseWebApi(config);
		}
	}
}