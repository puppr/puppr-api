namespace Puppr.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class challengephotoadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Battles", "ChallengerPhoto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Battles", "ChallengerPhoto");
        }
    }
}
