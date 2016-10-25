namespace Puppr.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Winsaddedtopets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pets", "Win", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pets", "Win");
        }
    }
}
