//
// BaseController.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FlexWork.Models;
using Microsoft.AspNet.Identity.Owin;

namespace FlexWorkApp.Controllers
{
    [Authorize]
    public class BaseController : ApiController
	{
		protected readonly FlexWorkContext Context = new FlexWorkContext();
    }
}