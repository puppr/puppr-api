namespace Puppr.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class petphotoscontroller : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PetPhotoes",
                c => new
                    {
                        PetPhotoId = c.Int(nullable: false, identity: true),
                        PetId = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.PetPhotoId)
                .ForeignKey("dbo.Pets", t => t.PetId, cascadeDelete: true)
                .Index(t => t.PetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PetPhotoes", "PetId", "dbo.Pets");
            DropIndex("dbo.PetPhotoes", new[] { "PetId" });
            DropTable("dbo.PetPhotoes");
        }
    }
}
