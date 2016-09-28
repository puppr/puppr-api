namespace Puppr.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PetBreeds", "Pet_PetId", "dbo.Pets");
            DropForeignKey("dbo.PetBreeds", "Breed_BreedId", "dbo.Breeds");
            DropForeignKey("dbo.Battles", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Battles", new[] { "Category_CategoryId" });
            DropIndex("dbo.PetBreeds", new[] { "Pet_PetId" });
            DropIndex("dbo.PetBreeds", new[] { "Breed_BreedId" });
            RenameColumn(table: "dbo.Battles", name: "Category_CategoryId", newName: "CategoryId");
            AddColumn("dbo.Pets", "BreedId", c => c.Int(nullable: false));
            AlterColumn("dbo.Battles", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Battles", "CategoryId");
            CreateIndex("dbo.Pets", "BreedId");
            AddForeignKey("dbo.Pets", "BreedId", "dbo.Breeds", "BreedId", cascadeDelete: true);
            AddForeignKey("dbo.Battles", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            DropTable("dbo.PetBreeds");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PetBreeds",
                c => new
                    {
                        Pet_PetId = c.Int(nullable: false),
                        Breed_BreedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pet_PetId, t.Breed_BreedId });
            
            DropForeignKey("dbo.Battles", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Pets", "BreedId", "dbo.Breeds");
            DropIndex("dbo.Pets", new[] { "BreedId" });
            DropIndex("dbo.Battles", new[] { "CategoryId" });
            AlterColumn("dbo.Battles", "CategoryId", c => c.Int());
            DropColumn("dbo.Pets", "BreedId");
            RenameColumn(table: "dbo.Battles", name: "CategoryId", newName: "Category_CategoryId");
            CreateIndex("dbo.PetBreeds", "Breed_BreedId");
            CreateIndex("dbo.PetBreeds", "Pet_PetId");
            CreateIndex("dbo.Battles", "Category_CategoryId");
            AddForeignKey("dbo.Battles", "Category_CategoryId", "dbo.Categories", "CategoryId");
            AddForeignKey("dbo.PetBreeds", "Breed_BreedId", "dbo.Breeds", "BreedId", cascadeDelete: true);
            AddForeignKey("dbo.PetBreeds", "Pet_PetId", "dbo.Pets", "PetId", cascadeDelete: true);
        }
    }
}
