namespace Cinema.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActorTableUpdate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Actors", new[] { "FilmCrewDto_Id" });
            AlterColumn("dbo.Actors", "FilmCrewDto_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Actors", "FilmCrewDto_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Actors", new[] { "FilmCrewDto_Id" });
            AlterColumn("dbo.Actors", "FilmCrewDto_Id", c => c.Int());
            CreateIndex("dbo.Actors", "FilmCrewDto_Id");
        }
    }
}
