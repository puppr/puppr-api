using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Puppr.API.Models
{
    public class Battle
       
    {
        public Battle()
        {
            Votes = new Collection<BattleVote>();
        }

        public int BattleId { get; set; }
        public int CategoryId { get; set; }
        public string ChallengerPhoto { get; set; }
        public int PetOneId { get; set; }
        public int PetTwoId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int PetOneVotes
        {
            get
            {
                return Votes.Count(v => v.PetId == PetOneId);
            }
        }
        public int PetTwoVotes
        {
            get
            {
                return Votes.Count(v => v.PetId == PetTwoId);
            }
        }
        public virtual Category Category { get; set; }
        public virtual Pet Pet { get; set; }
        public virtual ICollection<BattleVote> Votes { get; set; }




    }
}