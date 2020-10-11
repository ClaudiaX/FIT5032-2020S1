namespace UnPeu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookEvents", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookEvents", "BranchEventId", "dbo.BranchEvents");
            DropIndex("dbo.BookEvents", new[] { "BranchEventId" });
            DropIndex("dbo.BookEvents", new[] { "ApplicationUserId" });
            DropTable("dbo.BookEvents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BranchEventId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.BookEvents", "ApplicationUserId");
            CreateIndex("dbo.BookEvents", "BranchEventId");
            AddForeignKey("dbo.BookEvents", "BranchEventId", "dbo.BranchEvents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BookEvents", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
