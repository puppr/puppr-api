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

        [Authorize]
        [Route("api/owners/me")]
        public IHttpActionResult GetCurrentOwner()
        {
            var owner = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            return Ok(new
            {
                owner.Biography,
                owner.Email,
                owner.FirstName,
                owner.LastName,
                owner.Photo,
                owner.UserName,
                owner.Pets
            });
        }

        // GET: api/Owners/1
        [Authorize]
        public IHttpActionResult GetOwner(string id)
        {
            if(!OwnerExists(id))
            {
                return NotFound();
            }

            var owner = db.Users.Find(id);

            return Ok(new
            {
                owner.Biography,
                owner.Email,
                owner.FirstName,
                owner.LastName,
                owner.Id,
                owner.Photo,
                owner.UserName,
                owner.Pets
            });
        }

        // PUT: api/Owners/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOwner(string id, Owner.Changes owner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            owner.Id = id;

            if (id != owner.Id)
            {
                return BadRequest();
            }

            var user = db.Users.FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            if(id != user.Id)
            {
                return BadRequest();
            }

            var dbOwner = db.Users.Find(id);

            db.Entry(dbOwner).CurrentValues.SetValues(owner);
            db.Entry(dbOwner).State = EntityState.Modified;

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

        // GET: api/me/pets
        [Authorize]
        [Route("api/me/pets")]
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