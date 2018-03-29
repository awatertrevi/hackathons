//
// BaseController.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FoundIt.Webservice.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FoundIt.Webservice.Controllers
{
    [Authorize]
    public class BaseController : ApiController
    {
        protected readonly LostItContext Context = new LostItContext();
        protected LostItUserManager UserManager => Request.GetOwinContext().GetUserManager<LostItUserManager>();
    }
}
