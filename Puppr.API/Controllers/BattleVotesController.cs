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
    public class BattleVotesController : ApiController
    {
        private PupprDataContext db = new PupprDataContext();

        // GET: api/BattleVotes
        public IQueryable<BattleVote> GetBattleVotes()
        {
            return db.BattleVotes;
        }

        // GET: api/BattleVotes/5
        [ResponseType(typeof(BattleVote))]
        public IHttpActionResult GetBattleVote(int id)
        {
            BattleVote battleVote = db.BattleVotes.Find(id);
            if (battleVote == null)
            {
                return NotFound();
            }

            return Ok(battleVote);
        }

        // PUT: api/BattleVotes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBattleVote(int id, BattleVote battleVote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != battleVote.BattleVoteId)
            {
                return BadRequest();
            }

            db.Entry(battleVote).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BattleVoteExists(id))
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

        // POST: api/BattleVotes
        [ResponseType(typeof(BattleVote))]
        public IHttpActionResult PostBattleVote(BattleVote battleVote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BattleVotes.Add(battleVote);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = battleVote.BattleVoteId }, battleVote);
        }

        // DELETE: api/BattleVotes/5
        [ResponseType(typeof(BattleVote))]
        public IHttpActionResult DeleteBattleVote(int id)
        {
            BattleVote battleVote = db.BattleVotes.Find(id);
            if (battleVote == null)
            {
                return NotFound();
            }

            db.BattleVotes.Remove(battleVote);
            db.SaveChanges();

            return Ok(battleVote);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BattleVoteExists(int id)
        {
            return db.BattleVotes.Count(e => e.BattleVoteId == id) > 0;
        }
    }
}