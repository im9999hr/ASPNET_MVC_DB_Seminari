namespace SeminarUpisi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Predbiljezba",
                c => new
                    {
                        IdPredbiljezba = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        Ime = c.String(nullable: false, maxLength: 25),
                        Prezime = c.String(nullable: false, maxLength: 25),
                        Adresa = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        Telefon = c.String(nullable: false, maxLength: 50),
                        IdSeminar = c.Int(nullable: false),
                        Status = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.IdPredbiljezba)
                .ForeignKey("dbo.Seminar", t => t.IdSeminar, cascadeDelete: true)
                .Index(t => t.IdSeminar);
            
            CreateTable(
                "dbo.Seminar",
                c => new
                    {
                        IdSeminar = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false, maxLength: 100),
                        Opis = c.String(),
                        Datum = c.DateTime(nullable: false),
                        Predavac = c.String(maxLength: 50),
                        Popunjen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdSeminar);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Zaposlenik", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Zaposlenik",
                c => new
                    {
                        IdZaposlenik = c.String(nullable: false, maxLength: 128),
                        Ime = c.String(),
                        Prezime = c.String(),
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
                        KorisnickoIme = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.IdZaposlenik)
                .Index(t => t.KorisnickoIme, unique: true, name: "UserNameIndex");
            
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
                .ForeignKey("dbo.Zaposlenik", t => t.UserId, cascadeDelete: true)
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
                .ForeignKey("dbo.Zaposlenik", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.Zaposlenik");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Zaposlenik");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Zaposlenik");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Predbiljezba", "IdSeminar", "dbo.Seminar");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Zaposlenik", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Predbiljezba", new[] { "IdSeminar" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Zaposlenik");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Seminar");
            DropTable("dbo.Predbiljezba");
        }
    }
}
