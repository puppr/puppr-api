using Microsoft.AspNet.Identity.EntityFramework;
using Puppr.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Puppr.API.Infrastructure
{
    public class PupprDataContext : IdentityDbContext<Owner>
    {
        public PupprDataContext() : base("Puppr")
        {

        }
        
        public IDbSet<Battle> Battles { get; set; }
        public IDbSet<Breed> Breeds { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>()
                .HasMany(o => o.Pets)
                .WithRequired(p => p.Owner)
                .HasForeignKey(p => p.OwnerId);

            modelBuilder.Entity<Breed>()
                .HasMany(o => o.Pets)
                .WithRequired(p => p.Breed)
                .HasForeignKey(p => p.BreedId);

            modelBuilder.Entity<Category>()
                .HasMany(o => o.Battles)
                .WithRequired(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Pet>()
                .HasMany(o => o.PetPhotos)
                .WithRequired(p => p.Pet)
                .HasForeignKey(p => p.PetId);

            modelBuilder.Entity<Pet>()
                .HasMany(o => o.Battles)
                .WithRequired(p => p.Pet)
                .HasForeignKey(p => p.PetOneId);

            modelBuilder.Entity<Pet>()
                .HasMany(o => o.Battles)
                .WithRequired(p => p.Pet)
                .HasForeignKey(p => p.PetTwoId);

            modelBuilder.Entity<Battle>()
                .HasMany(o => o.Votes)
                .WithRequired(p => p.Battle)
                .HasForeignKey(p => p.BattleId);

            modelBuilder.Entity<Pet>()
                .HasMany(p => p.Votes)
                .WithRequired(v => v.Pet)
                .HasForeignKey(v => v.PetId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Puppr.API.Models.PetPhoto> PetPhotoes { get; set; }

        public System.Data.Entity.DbSet<Puppr.API.Models.BattleVote> BattleVotes { get; set; }
    }
}