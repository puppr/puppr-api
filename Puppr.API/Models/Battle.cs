using System.Collections.Generic;
using System.Linq;

namespace Puppr.API.Models
{
    public class Battle
    {
        public int BattleId { get; set; }
        public int CategoryId { get; set; }
        public int PetOneId { get; set; }
        public int PetTwoId { get; set; }

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