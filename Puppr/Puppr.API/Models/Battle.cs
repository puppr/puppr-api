namespace Puppr.API.Models
{
    public class Battle
    {
        public int BattleId { get; set; }
        public int CategoryId { get; set; }
        public int PetOneId { get; set; }
        public int PetTwoId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Pet Pet { get; set; }
    }
}