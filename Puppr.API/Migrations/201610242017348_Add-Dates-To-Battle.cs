namespace Puppr.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatesToBattle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Battles", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Battles", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Battles", "EndDate");
            DropColumn("dbo.Battles", "StartDate");
        }
    }
}
