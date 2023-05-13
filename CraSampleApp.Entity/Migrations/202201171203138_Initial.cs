namespace CraSampleApp.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieActorEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        MovieId = c.Guid(nullable: false),
                        ActorName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieEntities", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.MovieEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        MovieTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieTypeEntities", t => t.MovieTypeId, cascadeDelete: true)
                .Index(t => t.MovieTypeId);
            
            CreateTable(
                "dbo.MovieTypeEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieEntities", "MovieTypeId", "dbo.MovieTypeEntities");
            DropForeignKey("dbo.MovieActorEntities", "MovieId", "dbo.MovieEntities");
            DropIndex("dbo.MovieEntities", new[] { "MovieTypeId" });
            DropIndex("dbo.MovieActorEntities", new[] { "MovieId" });
            DropTable("dbo.MovieTypeEntities");
            DropTable("dbo.MovieEntities");
            DropTable("dbo.MovieActorEntities");
        }
    }
}
