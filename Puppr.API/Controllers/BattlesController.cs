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
    public class BattlesController : ApiController
    {
        private PupprDataContext db = new PupprDataContext();

        // GET: api/Battles
        public IQueryable<Battle> GetBattles()
        {
            return db.Battles;
        }

        // GET: api/Battles/5
        [ResponseType(typeof(Battle))]
        public IHttpActionResult GetBattle(int id)
        {
            Battle battle = db.Battles.Find(id);
            if (battle == null)
            {
                return NotFound();
            }

            return Ok(battle);
        }

        // PUT: api/Battles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBattle(int id, Battle battle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != battle.BattleId)
            {
                return BadRequest();
            }

            db.Entry(battle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BattleExists(id))
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

        // POST: api/Battles
        [ResponseType(typeof(Battle))]
        public IHttpActionResult PostBattle(Battle battle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Battles.Add(battle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = battle.BattleId }, battle);
        }

        // DELETE: api/Battles/5
        [ResponseType(typeof(Battle))]
        public IHttpActionResult DeleteBattle(int id)
        {
            Battle battle = db.Battles.Find(id);
            if (battle == null)
            {
                return NotFound();
            }

            db.Battles.Remove(battle);
            db.SaveChanges();

            return Ok(battle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BattleExists(int id)
        {
            return db.Battles.Count(e => e.BattleId == id) > 0;
        }
    }
}