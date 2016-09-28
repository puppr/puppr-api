namespace Puppr.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pets", "Owner_OwnerId", "dbo.Owners");
            DropIndex("dbo.Pets", new[] { "Owner_OwnerId" });
            RenameColumn(table: "dbo.Pets", name: "Owner_OwnerId", newName: "OwnerId");
            AlterColumn("dbo.Pets", "OwnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Pets", "OwnerId");
            AddForeignKey("dbo.Pets", "OwnerId", "dbo.Owners", "OwnerId", cascadeDelete: true);
            DropColumn("dbo.Pets", "PatientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pets", "PatientId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Pets", "OwnerId", "dbo.Owners");
            DropIndex("dbo.Pets", new[] { "OwnerId" });
            AlterColumn("dbo.Pets", "OwnerId", c => c.Int());
            RenameColumn(table: "dbo.Pets", name: "OwnerId", newName: "Owner_OwnerId");
            CreateIndex("dbo.Pets", "Owner_OwnerId");
            AddForeignKey("dbo.Pets", "Owner_OwnerId", "dbo.Owners", "OwnerId");
        }
    }
}
