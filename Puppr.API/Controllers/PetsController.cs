﻿using System;
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
    public class PetsController : SecuredController
    {
        // GET: api/Pets
        public IQueryable<Pet> GetPets()
        {
            return Db.Pets;
        }

        // GET: api/Pets/5
        [ResponseType(typeof(Pet))]
        public IHttpActionResult GetPet(int id)
        {
            Pet pet = Db.Pets.Find(id);
            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        // PUT: api/Pets/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPet(int id, Pet.Changes pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pet.PetId)
            {
                return BadRequest();
            }

            if(!CurrentUser.Pets.Any(p => p.PetId == id))
            {
                return BadRequest();
            }
            var dbPet = Db.Pets.Find(id);

            Db.Entry(dbPet).CurrentValues.SetValues(pet);

            Db.Entry(dbPet).State = EntityState.Modified;

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
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

        // POST: api/Pets
        [Authorize]
        [ResponseType(typeof(Pet))]
        public IHttpActionResult PostPet(Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pet.OwnerId = CurrentUser.Id;

            Db.Pets.Add(pet);
            Db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pet.PetId }, pet);
        }

        // DELETE: api/Pets/5
        [Authorize]
        [ResponseType(typeof(Pet))]
        public IHttpActionResult DeletePet(int id)
        {
            Pet pet = Db.Pets.Find(id);

            if(pet.OwnerId != CurrentUser.Id)
            {
                return NotFound();
            }

            if (pet == null)
            {
                return NotFound();
            }

            Db.Pets.Remove(pet);
            Db.SaveChanges();

            return Ok(pet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PetExists(int id)
        {
            return Db.Pets.Count(e => e.PetId == id) > 0;
        }
    }
}