namespace Puppr.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifthMigration : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Battles", "PetTwoId");
            AddForeignKey("dbo.Battles", "PetTwoId", "dbo.Pets", "PetId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Battles", "PetTwoId", "dbo.Pets");
            DropIndex("dbo.Battles", new[] { "PetTwoId" });
        }
    }
}
