namespace UnPeu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookEventModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BranchEventId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.BranchEvents", t => t.BranchEventId, cascadeDelete: true)
                .Index(t => t.BranchEventId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookEvents", "BranchEventId", "dbo.BranchEvents");
            DropForeignKey("dbo.BookEvents", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BookEvents", new[] { "ApplicationUserId" });
            DropIndex("dbo.BookEvents", new[] { "BranchEventId" });
            DropTable("dbo.BookEvents");
        }
    }
}
