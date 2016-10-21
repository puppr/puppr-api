using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Puppr.API.Models
{
    public class BattleVote
    {
        public int BattleVoteId { get; set; }

        public int BattleId { get; set; }

        public int PetId { get; set; }

        public virtual Battle Battle { get; set; }
        public virtual Pet Pet { get; set; }
    }
}