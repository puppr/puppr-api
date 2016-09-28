using System.Collections.Generic;

namespace Puppr.API.Models
{
    public class Breed
    {
        public int BreedId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}