namespace UnPeu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBranchEvents : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BranchEvents", "branchId", "dbo.Branches");
            DropForeignKey("dbo.BranchEvents", "EventTypeId", "dbo.EventTypes");
            DropIndex("dbo.BranchEvents", new[] { "EventTypeId" });
            DropIndex("dbo.BranchEvents", new[] { "branchId" });
            DropTable("dbo.BranchEvents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BranchEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventTypeId = c.Int(nullable: false),
                        branchId = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.BranchEvents", "branchId");
            CreateIndex("dbo.BranchEvents", "EventTypeId");
            AddForeignKey("dbo.BranchEvents", "EventTypeId", "dbo.EventTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BranchEvents", "branchId", "dbo.Branches", "Id", cascadeDelete: true);
        }
    }
}
