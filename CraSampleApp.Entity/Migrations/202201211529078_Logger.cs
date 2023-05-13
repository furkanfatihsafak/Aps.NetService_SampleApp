namespace CraSampleApp.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Logger : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Log = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogEntities");
        }
    }
}
