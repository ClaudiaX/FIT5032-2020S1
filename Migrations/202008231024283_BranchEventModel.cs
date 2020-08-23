namespace UnPeu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BranchEventModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BranchEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        Branch_Id = c.Int(),
                        EventType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.EventTypes", t => t.EventType_Id)
                .Index(t => t.Branch_Id)
                .Index(t => t.EventType_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BranchEvents", "EventType_Id", "dbo.EventTypes");
            DropForeignKey("dbo.BranchEvents", "Branch_Id", "dbo.Branches");
            DropIndex("dbo.BranchEvents", new[] { "EventType_Id" });
            DropIndex("dbo.BranchEvents", new[] { "Branch_Id" });
            DropTable("dbo.BranchEvents");
        }
    }
}
