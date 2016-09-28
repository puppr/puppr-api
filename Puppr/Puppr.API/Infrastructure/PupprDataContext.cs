using Puppr.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Puppr.API.Infrastructure
{
    public class PupprDataContext : DbContext
    {
        public PupprDataContext() : base("Puppr")
        {

        }
        
        public IDbSet<Battle> Battles { get; set; }
        public IDbSet<Breed> Breeds { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Owner> Owners { get; set; }
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
                .HasMany(o => o.Battles)
                .WithRequired(p => p.Pet)
                .HasForeignKey(p => p.PetOneId);

            modelBuilder.Entity<Pet>()
                .HasMany(o => o.Battles)
                .WithRequired(p => p.Pet)
                .HasForeignKey(p => p.PetTwoId);

        }
    }
}