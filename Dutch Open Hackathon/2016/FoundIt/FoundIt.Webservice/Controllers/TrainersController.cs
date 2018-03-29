using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FoundIt.Models;
using Microsoft.AspNet.Identity;

namespace FoundIt.Webservice.Controllers
{
    [RoutePrefix("trainers")]
    public class TrainersController : BaseController
    {
        [Route("me/workouts")]
        public IHttpActionResult GetMyWorkouts()
        {
            var userId = User.Identity.GetUserId();

            return Ok(Database.Workouts.Where(w => w.Trainer.UserId == userId));
        }

        [HttpPost]
        [Route("me/workouts")]
        public async Task<IHttpActionResult> AddWorkout(Workout workout)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            if (workout.StartDateTime > workout.EndDateTime)
                return BadRequest("The start datetime can't be later then the end datetime.");

            var trainer = GetTrainer();

            if (trainer == null)
                return BadRequest("You are not a trainer!");

            workout.Trainer = trainer;

            Database.Workouts.Add(workout);

            await Database.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("me/workouts/{id}")]
        public async Task<IHttpActionResult> EditWorkout(Workout workout, Guid id)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            var trainer = GetTrainer();

            if (trainer == null)
                return BadRequest("You are not a trainer!");

            if (workout.TrainerId != trainer.UserId)
                return BadRequest("This isn't your workout.");

            if (workout.StartDateTime > workout.EndDateTime)
                return BadRequest("The start datetime can't be later then the end datetime.");

            if (workout.Id != id)
                return BadRequest("The workout you are trying to edit isn't the one you sent along.");

            Database.Workouts.Attach(workout);

            var entry = Database.Entry(workout);

            entry.Property(e => e.Title).IsModified = true;
            entry.Property(e => e.Description).IsModified = true;
            entry.Property(e => e.StartDateTime).IsModified = true;
            entry.Property(e => e.EndDateTime).IsModified = true;
            entry.Property(e => e.Address).IsModified = true;
            entry.Property(e => e.Latitude).IsModified = true;
            entry.Property(e => e.Longitude).IsModified = true;
            entry.Property(e => e.FitCoins).IsModified = true;
            entry.Property(e => e.AvailableSeats).IsModified = true;
            entry.Property(e => e.CategoryId).IsModified = true;

            try
            {
                await Database.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (WorkoutExists(id) == false)
                    return NotFound();

                throw;
            }
            return Ok();
        }


        [HttpDelete]
        [Route("me/workouts/{id}")]
        public async Task<IHttpActionResult> DeleteWorkou(Guid id)
        {
            var trainer = GetTrainer();

            if (trainer == null)
                return BadRequest("You are not a trainer!");

            var workout = await Database.Workouts.FindAsync(id);

            if (workout == null)
                return NotFound();

            if(workout.Trainer.UserId != trainer.UserId)
                return BadRequest("This isn't your workout");

            Database.Workouts.Remove(workout);

            await Database.SaveChangesAsync();

            return Ok();
        }
        
        [Route("{id}/workouts")]
        public IHttpActionResult GetTrainerWorkouts(Guid id)
        {
            return Ok(Database.Workouts.Where(w => w.Trainer.UserId == id.ToString()));
        }

        private Trainer GetTrainer()
        {
            var userId = User.Identity.GetUserId();
            var trainer = Database.Trainers.FirstOrDefault(t => t.UserId == userId);

            return trainer;
        }

        private bool WorkoutExists(Guid id)
        {
            return Database.Workouts.Count(w => w.Id == id) > 0;
        }
    }
}
