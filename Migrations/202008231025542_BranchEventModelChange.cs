namespace UnPeu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BranchEventModelChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BranchEvents", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.BranchEvents", "EventType_Id", "dbo.EventTypes");
            DropIndex("dbo.BranchEvents", new[] { "Branch_Id" });
            DropIndex("dbo.BranchEvents", new[] { "EventType_Id" });
            RenameColumn(table: "dbo.BranchEvents", name: "Branch_Id", newName: "BranchId");
            RenameColumn(table: "dbo.BranchEvents", name: "EventType_Id", newName: "EventTypeId");
            AlterColumn("dbo.BranchEvents", "BranchId", c => c.Int(nullable: false));
            AlterColumn("dbo.BranchEvents", "EventTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.BranchEvents", "EventTypeId");
            CreateIndex("dbo.BranchEvents", "BranchId");
            AddForeignKey("dbo.BranchEvents", "BranchId", "dbo.Branches", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BranchEvents", "EventTypeId", "dbo.EventTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BranchEvents", "EventTypeId", "dbo.EventTypes");
            DropForeignKey("dbo.BranchEvents", "BranchId", "dbo.Branches");
            DropIndex("dbo.BranchEvents", new[] { "BranchId" });
            DropIndex("dbo.BranchEvents", new[] { "EventTypeId" });
            AlterColumn("dbo.BranchEvents", "EventTypeId", c => c.Int());
            AlterColumn("dbo.BranchEvents", "BranchId", c => c.Int());
            RenameColumn(table: "dbo.BranchEvents", name: "EventTypeId", newName: "EventType_Id");
            RenameColumn(table: "dbo.BranchEvents", name: "BranchId", newName: "Branch_Id");
            CreateIndex("dbo.BranchEvents", "EventType_Id");
            CreateIndex("dbo.BranchEvents", "Branch_Id");
            AddForeignKey("dbo.BranchEvents", "EventType_Id", "dbo.EventTypes", "Id");
            AddForeignKey("dbo.BranchEvents", "Branch_Id", "dbo.Branches", "Id");
        }
    }
}
