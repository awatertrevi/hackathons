using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexWorkApp.Controllers;
using Microsoft.AspNet.Identity;

namespace FlexWork.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : BaseController
    {
        [HttpGet]
        [Route("me")]
        public IHttpActionResult GetMe()
        {
            var userId = User.Identity.GetUserId();

            return Ok(Context.Users.Single(u => u.Id == userId));
        }
    }
}
