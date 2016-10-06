namespace Puppr.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pets", "PatientId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pets", "PatientId");
        }
    }
}
