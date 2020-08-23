namespace UnPeu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReAddBranchEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BranchEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventTypeId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.EventTypes", t => t.EventTypeId, cascadeDelete: true)
                .Index(t => t.EventTypeId)
                .Index(t => t.BranchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BranchEvents", "EventTypeId", "dbo.EventTypes");
            DropForeignKey("dbo.BranchEvents", "BranchId", "dbo.Branches");
            DropIndex("dbo.BranchEvents", new[] { "BranchId" });
            DropIndex("dbo.BranchEvents", new[] { "EventTypeId" });
            DropTable("dbo.BranchEvents");
        }
    }
}
