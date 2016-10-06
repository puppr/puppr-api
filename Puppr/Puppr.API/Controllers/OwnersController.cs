using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Puppr.API.Infrastructure;
using Puppr.API.Models;

namespace Puppr.API.Controllers
{
    public class OwnersController : ApiController
    {
        private PupprDataContext db = new PupprDataContext();

        // PUT: api/Owners/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOwner(string id, Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != owner.Id)
            {
                return BadRequest();
            }

            var user = db.Users.FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            if(id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(owner).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/Owners/5/pets
        [Authorize]
        public dynamic GetPetsForOwner()
        {
            // Get the currently logged in user
                // By checking the username against the token
            var user = db.Users.FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            // Return pets where that pets owner id matches the currently logged in user id.
            return db.Pets.Where(p => p.OwnerId == user.Id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OwnerExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}