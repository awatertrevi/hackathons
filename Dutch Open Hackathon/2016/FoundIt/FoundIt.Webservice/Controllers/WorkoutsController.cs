using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoundIt.Webservice.Controllers
{
    [RoutePrefix("workouts")]
    public class WorkoutsController : BaseController
    {
        [AllowAnonymous]
        [Route("categories")]
        public IHttpActionResult GetCategories()
        {
            return Ok(Database.WorkoutCategories);
        }

        [AllowAnonymous]
        [Route("nearby/{latitude}/{longitude}/{distance}")]
        public IHttpActionResult GetNearbyWorkouts(double latitude, double longitude, double distance)
        {
            if (distance > 15)
                return BadRequest("The maximum distance is 15km.");

            var distanceInMeters = distance * 1000;

            var coord = new GeoCoordinate(latitude, longitude);
            var nearbyWorkouts = Database.Workouts.Include(w => w.Category).Include(w => w.Trainer).Select(w => new
            {
                workout = w,
                category = w.Category,
                trainer = w.Trainer,
                geocoord = new GeoCoordinate { Latitude = (double?)w.Latitude ?? 0, Longitude = (double?)w.Longitude ?? 0 }

            }).AsEnumerable().Where(x => x.geocoord.GetDistanceTo(coord) < distanceInMeters).Select(x => { x.workout.Category = x.category; x.workout.Trainer = x.trainer; return x.workout; }).ToList();

            return Ok(nearbyWorkouts);
        }

        [AllowAnonymous]
        [Route("seats/{id}")]
        public IHttpActionResult GetSeatsLeft(Guid id)
        {
            var workout = Database.Workouts.FirstOrDefault(w => w.Id == id);

            if (workout == null)
                return NotFound();

            return Ok(workout.AvailableSeats - Database.WorkoutRegistrations.Count(wr => wr.Workout.Id == workout.Id));
        }
    }
}
