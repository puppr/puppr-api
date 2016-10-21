namespace Puppr.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class genderadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pets", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pets", "Gender");
        }
    }
}
