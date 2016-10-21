using System;
using System.Collections.Generic;

namespace Puppr.API.Models
{
    public class Pet
    {
        public int PetId { get; set; }
        public string OwnerId { get; set; }

        public int BreedId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DogFood { get; set; }
        public string Toy { get; set; }
        public string Activity { get; set; }
        public string Gender { get; set; }

        public virtual Owner Owner { get; set; }
        public virtual Breed Breed { get; set; }
        public virtual ICollection<Battle> Battles { get; set; }
        public virtual ICollection<PetPhoto> PetPhotos { get; set; }

        public virtual ICollection<BattleVote> Votes { get; set; }
    }
}