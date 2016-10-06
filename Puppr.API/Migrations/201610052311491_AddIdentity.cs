namespace Puppr.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdentity : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Owners", newName: "AspNetUsers");
            DropForeignKey("dbo.Pets", "OwnerId", "dbo.Owners");
            DropIndex("dbo.Pets", new[] { "OwnerId" });
            DropPrimaryKey("dbo.AspNetUsers");
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            AddColumn("dbo.AspNetUsers", "Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 256));
            AddColumn("dbo.AspNetUsers", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PasswordHash", c => c.String());
            AddColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Pets", "OwnerId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.AspNetUsers", "Id");
            CreateIndex("dbo.Pets", "OwnerId");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            AddForeignKey("dbo.Pets", "OwnerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "OwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "OwnerId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Pets", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Pets", new[] { "OwnerId" });
            DropPrimaryKey("dbo.AspNetUsers");
            AlterColumn("dbo.Pets", "OwnerId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "UserName");
            DropColumn("dbo.AspNetUsers", "AccessFailedCount");
            DropColumn("dbo.AspNetUsers", "LockoutEnabled");
            DropColumn("dbo.AspNetUsers", "LockoutEndDateUtc");
            DropColumn("dbo.AspNetUsers", "TwoFactorEnabled");
            DropColumn("dbo.AspNetUsers", "PhoneNumberConfirmed");
            DropColumn("dbo.AspNetUsers", "PhoneNumber");
            DropColumn("dbo.AspNetUsers", "SecurityStamp");
            DropColumn("dbo.AspNetUsers", "PasswordHash");
            DropColumn("dbo.AspNetUsers", "EmailConfirmed");
            DropColumn("dbo.AspNetUsers", "Email");
            DropColumn("dbo.AspNetUsers", "Id");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            AddPrimaryKey("dbo.AspNetUsers", "OwnerId");
            CreateIndex("dbo.Pets", "OwnerId");
            AddForeignKey("dbo.Pets", "OwnerId", "dbo.Owners", "OwnerId", cascadeDelete: true);
            RenameTable(name: "dbo.AspNetUsers", newName: "Owners");
        }
    }
}
