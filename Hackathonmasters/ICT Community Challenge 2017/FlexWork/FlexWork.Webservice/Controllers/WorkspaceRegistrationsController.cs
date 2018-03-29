using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using FlexWork.Models;
using FlexWorkApp.Controllers;
using Microsoft.AspNet.Identity;

namespace FlexWork.Controllers
{
    public class WorkspaceRegistrationsController : BaseController
    {
        public async Task<IHttpActionResult> AddMyWorkspaceRegistration(WorkspaceRegistration workspaceRegistration)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var userId = User.Identity.GetUserId();

            if (workspaceRegistration.UserId != userId)
                return Unauthorized();

            if (Context.WorkspaceRegistrations.Any(wr => wr.UserId == userId && wr.WorkspaceId == workspaceRegistration.WorkspaceId))
                return BadRequest("You already checked in at this workspace.");

            workspaceRegistration.User = null;
            workspaceRegistration.Workspace = null;

            var workspace = await Context.Workspaces.FindAsync(workspaceRegistration.WorkspaceId);

            if (workspace == null)
                return NotFound();

            if (workspace.IsPermanentlyClosed)
                return BadRequest("You can't check in at a workspace which is permanently closed.");

            if (workspace.OpeningHours.Any(oh => oh.WorkspaceId == workspace.Id && oh.IsClosed == false))
                return BadRequest("You can't check in at a workspace which is currently closed.");

            Context.WorkspaceRegistrations.Add(workspaceRegistration);
            await Context.SaveChangesAsync();

            return Ok(workspaceRegistration);
        }

        [HttpDelete]
        [Route("me/{id}")]
        public async Task<IHttpActionResult> DeleteMyWorkspaceRegistrations(Guid id)
        {
            var userId = User.Identity.GetUserId();

            var workspaceRegistration = Context.WorkspaceRegistrations.Include(wr => wr.Workspace).SingleOrDefault(wr => wr.UserId == userId && wr.WorkspaceId == id);

            if (workspaceRegistration == null)
                return NotFound();

            if (workspaceRegistration.UserId != userId)
                return Unauthorized();

            //if (workspaceRegistration.Workspace.OpeningHours.Any(oh => oh.OpenTime <= ) < DateTime.UtcNow.AddDays(1)) // TODO: fix this
             //   return BadRequest("You can only cancel a workout 24h before it starts.");

            Context.WorkspaceRegistrations.Remove(workspaceRegistration);

            await Context.SaveChangesAsync();

            return Ok();
        }
    }
}
