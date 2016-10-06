namespace Puppr.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Battles",
                c => new
                    {
                        BattleId = c.Int(nullable: false, identity: true),
                        PetOneId = c.Int(nullable: false),
                        PetTwoId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.BattleId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Breeds",
                c => new
                    {
                        BreedId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BreedId);
            
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        PetId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        DogFood = c.String(),
                        Toy = c.String(),
                        Activity = c.String(),
                        Owner_OwnerId = c.Int(),
                    })
                .PrimaryKey(t => t.PetId)
                .ForeignKey("dbo.Owners", t => t.Owner_OwnerId)
                .Index(t => t.Owner_OwnerId);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        OwnerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Biography = c.String(),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.OwnerId);
            
            CreateTable(
                "dbo.PetBreeds",
                c => new
                    {
                        Pet_PetId = c.Int(nullable: false),
                        Breed_BreedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pet_PetId, t.Breed_BreedId })
                .ForeignKey("dbo.Pets", t => t.Pet_PetId, cascadeDelete: true)
                .ForeignKey("dbo.Breeds", t => t.Breed_BreedId, cascadeDelete: true)
                .Index(t => t.Pet_PetId)
                .Index(t => t.Breed_BreedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pets", "Owner_OwnerId", "dbo.Owners");
            DropForeignKey("dbo.PetBreeds", "Breed_BreedId", "dbo.Breeds");
            DropForeignKey("dbo.PetBreeds", "Pet_PetId", "dbo.Pets");
            DropForeignKey("dbo.Battles", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.PetBreeds", new[] { "Breed_BreedId" });
            DropIndex("dbo.PetBreeds", new[] { "Pet_PetId" });
            DropIndex("dbo.Pets", new[] { "Owner_OwnerId" });
            DropIndex("dbo.Battles", new[] { "Category_CategoryId" });
            DropTable("dbo.PetBreeds");
            DropTable("dbo.Owners");
            DropTable("dbo.Pets");
            DropTable("dbo.Breeds");
            DropTable("dbo.Categories");
            DropTable("dbo.Battles");
        }
    }
}
