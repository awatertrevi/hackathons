using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FlexWork.Models;
using FlexWorkApp.Controllers;
using Microsoft.AspNet.Identity;

namespace FlexWork.Controllers
{
    [RoutePrefix("work-spaces")]
    public class WorkspacesController : BaseController
    {
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetWorkspace(Guid id)
        {
            var workspace = Context.Workspaces.Include(w => w.OpeningHours).Include(w => w.WorkspaceFacilities).SingleOrDefault(w => w.Id == id);

            if (workspace == null)
                return NotFound();

            foreach (var workspaceFacility in workspace.WorkspaceFacilities)
            {
                workspaceFacility.Facility = Context.Facilities.Single(f => f.Id == workspaceFacility.FacilityId);
            }

            return Ok(workspace);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("nearby/{latitude}/{longitude}/{distance}")]
        public IHttpActionResult GetNearbyWorkspaces(double latitude, double longitude, double distance)
        {
            if (distance > 15)
                return BadRequest("The maximum distance is 15km.");

            var queryLocation = DbGeography.PointFromText("POINT(" + longitude + " " + latitude + ")", 4326);

            var distanceInMeters = distance * 1000;

            var nearbyWorkspaces =
                Context.Workspaces.Include(w => w.WorkspaceFacilities).Include(w => w.OpeningHours).Where(w =>
                    DbGeography.PointFromText("POINT(" + w.Longitude + " " + w.Latitude + ")", 4326).Distance(queryLocation) < distanceInMeters
                    && w.OpeningHours.Any(oh => oh.WorkspaceId == w.Id && oh.IsClosed == false)) // TODO: Check close and open dates.
                    .ToArray();

            foreach (var workspace in nearbyWorkspaces)
            {
                foreach (var workspaceFacility in workspace.WorkspaceFacilities)
                {
                    workspaceFacility.Facility = Context.Facilities.Single(f => f.Id == workspaceFacility.FacilityId);
                }
            }
            
            return Ok(nearbyWorkspaces);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("recent")]
        public IHttpActionResult GetRecentCheckedInWorkspaces()
        {
            var userId = User.Identity.GetUserId();

            var recentRegistrations = Context.WorkspaceRegistrations.Include(wr => wr.Workspace).Where(wr => wr.UserId == userId).OrderByDescending(wr => wr.CheckInDateTime).Take(20);

            var recentRegistrationWorkspaces = new List<Workspace>();

            foreach (var recentRegistration in recentRegistrations)
            {
                recentRegistrationWorkspaces.Add(recentRegistration.Workspace);
            }

            var recentCheckedInWorkspaces = Context.Workspaces.Include(w => w.OpeningHours).Include(w => w.WorkspaceFacilities).Where(w => recentRegistrations.Any(wr => wr.WorkspaceId == w.Id)).ToArray();

            foreach (var workspace in recentRegistrationWorkspaces)
            {
                foreach (var workspaceFacility in workspace.WorkspaceFacilities)
                {
                    workspaceFacility.Facility = Context.Facilities.Single(f => f.Id == workspaceFacility.FacilityId);
                }
            }

            return Ok(recentCheckedInWorkspaces);
        }
    }
}
