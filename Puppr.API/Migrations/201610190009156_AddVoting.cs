namespace Puppr.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVoting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BattleVotes",
                c => new
                    {
                        BattleVoteId = c.Int(nullable: false, identity: true),
                        BattleId = c.Int(nullable: false),
                        PetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BattleVoteId)
                .ForeignKey("dbo.Pets", t => t.PetId)
                .ForeignKey("dbo.Battles", t => t.BattleId, cascadeDelete: true)
                .Index(t => t.BattleId)
                .Index(t => t.PetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BattleVotes", "BattleId", "dbo.Battles");
            DropForeignKey("dbo.BattleVotes", "PetId", "dbo.Pets");
            DropIndex("dbo.BattleVotes", new[] { "PetId" });
            DropIndex("dbo.BattleVotes", new[] { "BattleId" });
            DropTable("dbo.BattleVotes");
        }
    }
}
