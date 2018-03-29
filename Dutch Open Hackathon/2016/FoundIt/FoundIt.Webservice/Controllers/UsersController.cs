//
// TrainersController.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using FoundIt.Models;
using Microsoft.AspNet.Identity;

namespace FoundIt.Webservice.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : BaseController
    {
        [Route("me")]
        public IHttpActionResult GetMe()
        {
            var userId = User.Identity.GetUserId();

            return Ok(Context.Users.Single(u => u.Id == userId));
        }
        
        [Route("{id}")]
        public async Task<IHttpActionResult> GetUser(Guid id)
        {
            var user = await Context.Users.SingleOrDefaultAsync(u => u.Id == id.ToString());

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        #region LostItems
        [Route("me/lostitems")]
        public IHttpActionResult GetMyLostItems()
        {
            var userId = User.Identity.GetUserId();

            return Ok(Context.LostItems.Include(i => i.Reporter).Where(i => i.Reporter.Id == userId));
        }

        [HttpPost]
        [Route("me/lostitems")]
        public async Task<IHttpActionResult> AddLostItem(LostItem lostItem)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            lostItem.Reporter = GetUser();

            Context.LostItems.Add(lostItem);

            await Context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("me/lostitems/{id}")]
        public async Task<IHttpActionResult> EditLostItem(LostItem lostItem, Guid id)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            if (lostItem.Reporter.Id != User.Identity.GetUserId())
                return BadRequest("This isn't your lostItem.");

            if (lostItem.Id != id)
                return BadRequest("The lostItem you are trying to edit isn't the one you sent along.");

            Context.LostItems.Attach(lostItem);

            var entry = Context.Entry(lostItem);

            entry.Property(e => e.Description).IsModified = true;
            entry.Property(e => e.EstimatedLoseTime).IsModified = true;
            entry.Property(e => e.Latitude).IsModified = true;
            entry.Property(e => e.Longitude).IsModified = true;
            entry.Property(e => e.LoseAddress).IsModified = true;
            entry.Property(e => e.Name).IsModified = true;
            entry.Property(e => e.Radius).IsModified = true;

            try
            {
                await Context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (LostItemExists(id) == false)
                    return NotFound();

                throw;
            }
            return Ok();
        }


        [HttpDelete]
        [Route("me/lostitems/{id}")]
        public async Task<IHttpActionResult> DeleteLostItem(Guid id)
        {
            var lostitem = await Context.LostItems.Include(i => i.Reporter).SingleOrDefaultAsync(i => i.Id == id);

            if (lostitem == null)
                return NotFound();

            if (lostitem.Reporter.Id != User.Identity.GetUserId())
                return BadRequest("This isn't your lostitem");

            Context.LostItems.Remove(lostitem);

            await Context.SaveChangesAsync();

            return Ok();
        }

        [Route("{id}/lostitems")]
        public IHttpActionResult GetLostItems(Guid id)
        {
            return Ok(Context.LostItems.Include(i => i.Reporter).Where(w => w.Reporter.Id == id.ToString()));
        }

        [Route("me/lostitems/found")]
        public IHttpActionResult GetFoundLostItems()
        {
            string userid = User.Identity.GetUserId();
            return Ok(Context.FoundItems.Include(i => i.Finder).Include(i => i.LostItem).Where(fi => fi.LostItem.Reporter.Id == userid));
        }



        [Route("me/lostitems/matches")]
        public IHttpActionResult GetFoundLostItemMatches()
        {
            string userid = User.Identity.GetUserId();

            var q = from li in Context.LostItems
                    from fi in Context.FoundItems
                    where li.Reporter.Id == userid
                    where fi.LostItem.Reporter == null
                    where li.Name == fi.LostItem.Name

                    where System.Data.Entity.Spatial.DbGeography.PointFromText("POINT(" + fi.Longitude + " " + fi.Latitude + ")", 4326).Distance(System.Data.Entity.Spatial.DbGeography.PointFromText("POINT(" + li.Longitude + " " + li.Latitude + ")", 4326)) < (li.Radius??10+1)*1000

                    select fi;
            
            return Ok(q.Include(i => i.LostItem));
        }

        private bool LostItemExists(Guid id)
        {
            return Context.LostItems.Any(w => w.Id == id);
        }
        #endregion

        #region FoundItems
        [Route("me/founditems")]
        public IHttpActionResult GetMyFoundItems()
        {
            var userId = User.Identity.GetUserId();

            return Ok(Context.FoundItems.Include(i => i.Finder).Where(i => i.Finder.Id == userId));
        }

        [HttpPost]
        [Route("me/founditems")]
        public async Task<IHttpActionResult> AddFoundItem(FoundItem foundItem)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            foundItem.Finder = GetUser();

            if (foundItem.LostItem.Reporter != null && foundItem.LostItem.Reporter.Id == foundItem.Finder.Id)
                foundItem.LostItem.Reporter = foundItem.Finder;

            using (var tran = Context.Database.BeginTransaction())
            {
                if (foundItem.LostItem.Id == Guid.Empty)
                {
                    Context.LostItems.Add(foundItem.LostItem);
                    await Context.SaveChangesAsync();
                } else
                {
                    Context.LostItems.Attach(foundItem.LostItem);
                    Context.Entry(foundItem.LostItem).Reload();
                }
                Context.FoundItems.Add(foundItem);

                await Context.SaveChangesAsync();
                tran.Commit();
            }

            return Ok();
        }

        [HttpPut]
        [Route("me/founditems/{id}")]
        public async Task<IHttpActionResult> EditFoundItem(FoundItem foundItem, Guid id)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            if (foundItem.Finder.Id != User.Identity.GetUserId())
                return BadRequest("This isn't your foundItem.");

            if (foundItem.Id != id)
                return BadRequest("The foundItem you are trying to edit isn't the one you sent along.");

            Context.FoundItems.Attach(foundItem);

            var entry = Context.Entry(foundItem);

            entry.Property(e => e.FindAddress).IsModified = true;
            entry.Property(e => e.FoundTime).IsModified = true;
            entry.Property(e => e.Latitude).IsModified = true;
            entry.Property(e => e.Longitude).IsModified = true;
            entry.Property(e => e.Picture).IsModified = true;
            entry.Property(e => e.LostItem).IsModified = true;

            try
            {
                await Context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (FoundItemExists(id) == false)
                    return NotFound();

                throw;
            }
            return Ok();
        }


        [HttpDelete]
        [Route("me/founditems/{id}")]
        public async Task<IHttpActionResult> DeleteFoundItem(Guid id)
        {
            var founditem = await Context.FoundItems.Include(i => i.Finder).SingleOrDefaultAsync(i => i.Id == id);

            if (founditem == null)
                return NotFound();

            if (founditem.Finder.Id != User.Identity.GetUserId())
                return BadRequest("This isn't your founditem");

            Context.FoundItems.Remove(founditem);

            await Context.SaveChangesAsync();

            return Ok();
        }

        [Route("{id}/founditems")]
        public IHttpActionResult GetFoundItems(Guid id)
        {
            return Ok(Context.FoundItems.Include(i => i.Finder).Where(w => w.Finder.Id == id.ToString()));
        }

        private bool FoundItemExists(Guid id)
        {
            return Context.FoundItems.Any(w => w.Id == id);
        }
        #endregion

        private User GetUser()
        {
            var userid = User.Identity.GetUserId();
            return Context.Users.SingleOrDefault(u => u.Id == userid);
        }
        

        //[HttpPost]
        //[Route("me/promote")]
        //public async Task<IHttpActionResult> PromoteToTrainer(Trainer trainer)
        //{
        //    if (ModelState.IsValid == false)
        //        return BadRequest(ModelState);

        //    var userId = User.Identity.GetUserId();
        //    var user = Database.Users.Where(u => u.Id == userId).Include(u => u.Trainer).First();

        //    if (user.IsTrainer)
        //        return BadRequest("User is already a trainer!");

        //    user.Trainer = trainer;

        //    await Database.SaveChangesAsync();

        //    return Ok();
        //}
    }
}
