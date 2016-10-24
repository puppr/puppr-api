namespace Puppr.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restartmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Battles",
                c => new
                    {
                        BattleId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        PetOneId = c.Int(nullable: false),
                        PetTwoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BattleId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Pets", t => t.PetTwoId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.PetTwoId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        PetId = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(nullable: false, maxLength: 128),
                        BreedId = c.Int(nullable: false),
                        Name = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        DogFood = c.String(),
                        Toy = c.String(),
                        Activity = c.String(),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.PetId)
                .ForeignKey("dbo.Breeds", t => t.BreedId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.BreedId);
            
            CreateTable(
                "dbo.Breeds",
                c => new
                    {
                        BreedId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BreedId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Biography = c.String(),
                        Photo = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.BattleVotes", "BattleId", "dbo.Battles");
            DropForeignKey("dbo.BattleVotes", "PetId", "dbo.Pets");
            DropForeignKey("dbo.PetPhotoes", "PetId", "dbo.Pets");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pets", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pets", "BreedId", "dbo.Breeds");
            DropForeignKey("dbo.Battles", "PetTwoId", "dbo.Pets");
            DropForeignKey("dbo.Battles", "CategoryId", "dbo.Categories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.BattleVotes", new[] { "PetId" });
            DropIndex("dbo.BattleVotes", new[] { "BattleId" });
            DropIndex("dbo.PetPhotoes", new[] { "PetId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Pets", new[] { "BreedId" });
            DropIndex("dbo.Pets", new[] { "OwnerId" });
            DropIndex("dbo.Battles", new[] { "PetTwoId" });
            DropIndex("dbo.Battles", new[] { "CategoryId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.BattleVotes");
            DropTable("dbo.PetPhotoes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Breeds");
            DropTable("dbo.Pets");
            DropTable("dbo.Categories");
            DropTable("dbo.Battles");
        }
    }
}
