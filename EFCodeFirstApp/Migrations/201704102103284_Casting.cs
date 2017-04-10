namespace EFCodeFirstApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Casting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        ActorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ActorId);
            
            CreateTable(
                "dbo.ActorMovies",
                c => new
                    {
                        ActorMovieId = c.Int(nullable: false, identity: true),
                        Character = c.String(),
                        MovieId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActorMovieId)
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.ActorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActorMovies", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.ActorMovies", "ActorId", "dbo.Actors");
            DropIndex("dbo.ActorMovies", new[] { "ActorId" });
            DropIndex("dbo.ActorMovies", new[] { "MovieId" });
            DropTable("dbo.ActorMovies");
            DropTable("dbo.Actors");
        }
    }
}
