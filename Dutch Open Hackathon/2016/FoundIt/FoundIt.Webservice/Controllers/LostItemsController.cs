using FoundIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace FoundIt.Webservice.Controllers
{

    [RoutePrefix("lostitem")]
    public class LostItemsController: BaseController
    {
        [Route("nearby/{latitude}/{longitude}/{distance}")]
        public IHttpActionResult GetNearbyLostItems(double latitude, double longitude, double distance)
        {
            if (distance > 15)
                return BadRequest("The maximum distance is 15km.");
         
            var queryLocation = System.Data.Entity.Spatial.DbGeography.PointFromText("POINT(" + longitude + " " + latitude + ")", 4326);

            var distanceInMeters = distance * 1000;
            var nearbyWorkouts = Context.LostItems.Include(i => i.Reporter)
                .Where(i => System.Data.Entity.Spatial.DbGeography.PointFromText("POINT("+ i.Longitude+" "+ i.Latitude+")", 4326).Distance(queryLocation) < distanceInMeters)
                .ToList();
            
            return Ok(nearbyWorkouts);
        }
    }
}