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
    public class PetPhotoesController : ApiController
    {
        private PupprDataContext db = new PupprDataContext();

        // GET: api/PetPhotoes
        public IQueryable<PetPhoto> GetPetPhotoes()
        {
            return db.PetPhotoes;
        }

        // GET: api/PetPhotoes/5
        [ResponseType(typeof(PetPhoto))]
        public IHttpActionResult GetPetPhoto(int id)
        {
            PetPhoto petPhoto = db.PetPhotoes.Find(id);
            if (petPhoto == null)
            {
                return NotFound();
            }

            return Ok(petPhoto);
        }

        // PUT: api/PetPhotoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPetPhoto(int id, PetPhoto petPhoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != petPhoto.PetPhotoId)
            {
                return BadRequest();
            }

            db.Entry(petPhoto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetPhotoExists(id))
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

        // POST: api/PetPhotoes
        [ResponseType(typeof(PetPhoto))]
        public IHttpActionResult PostPetPhoto(PetPhoto petPhoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PetPhotoes.Add(petPhoto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = petPhoto.PetPhotoId }, petPhoto);
        }

        // DELETE: api/PetPhotoes/5
        [ResponseType(typeof(PetPhoto))]
        public IHttpActionResult DeletePetPhoto(int id)
        {
            PetPhoto petPhoto = db.PetPhotoes.Find(id);
            if (petPhoto == null)
            {
                return NotFound();
            }

            db.PetPhotoes.Remove(petPhoto);
            db.SaveChanges();

            return Ok(petPhoto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PetPhotoExists(int id)
        {
            return db.PetPhotoes.Count(e => e.PetPhotoId == id) > 0;
        }
    }
}