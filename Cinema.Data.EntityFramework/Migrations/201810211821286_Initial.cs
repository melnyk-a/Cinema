using System.Data.Entity.Migrations;

namespace Cinema.Data.EntityFramework.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilmCrews",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    DirectorDto_Id = c.Int(nullable: false),
                    FilmDto_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Directors", t => t.DirectorDto_Id, cascadeDelete: true)
                .ForeignKey("dbo.Films", t => t.FilmDto_Id, cascadeDelete: true)
                .Index(t => t.DirectorDto_Id)
                .Index(t => t.FilmDto_Id);

            CreateTable(
                "dbo.Actors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    HumanDto_Id = c.Int(nullable: false),
                    FilmCrewDto_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Humans", t => t.HumanDto_Id, cascadeDelete: true)
                .ForeignKey("dbo.FilmCrews", t => t.FilmCrewDto_Id)
                .Index(t => t.HumanDto_Id)
                .Index(t => t.FilmCrewDto_Id);

            CreateTable(
                "dbo.Humans",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Surname = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Directors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    HumanDto_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Humans", t => t.HumanDto_Id, cascadeDelete: true)
                .Index(t => t.HumanDto_Id);

            CreateTable(
                "dbo.Films",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    HasBlurayRelease = c.Boolean(),
                    Language = c.Int(nullable: false),
                    ReleaseDate = c.DateTime(nullable: false),
                    Title = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.FilmCrews", "FilmDto_Id", "dbo.Films");
            DropForeignKey("dbo.FilmCrews", "DirectorDto_Id", "dbo.Directors");
            DropForeignKey("dbo.Directors", "HumanDto_Id", "dbo.Humans");
            DropForeignKey("dbo.Actors", "FilmCrewDto_Id", "dbo.FilmCrews");
            DropForeignKey("dbo.Actors", "HumanDto_Id", "dbo.Humans");
            DropIndex("dbo.Directors", new[] { "HumanDto_Id" });
            DropIndex("dbo.Actors", new[] { "FilmCrewDto_Id" });
            DropIndex("dbo.Actors", new[] { "HumanDto_Id" });
            DropIndex("dbo.FilmCrews", new[] { "FilmDto_Id" });
            DropIndex("dbo.FilmCrews", new[] { "DirectorDto_Id" });
            DropTable("dbo.Films");
            DropTable("dbo.Directors");
            DropTable("dbo.Humans");
            DropTable("dbo.Actors");
            DropTable("dbo.FilmCrews");
        }
    }
}