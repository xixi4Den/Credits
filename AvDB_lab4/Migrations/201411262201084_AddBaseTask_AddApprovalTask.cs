namespace AvDB_lab4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBaseTask_AddApprovalTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseTask",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CreditApplicationId = c.Guid(nullable: false),
                        DispalyText = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CompleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CreditApplication", t => t.CreditApplicationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CreditApplicationId);
            
            CreateTable(
                "dbo.ApprovalTask",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApprovalType = c.Int(nullable: false),
                        Outcome = c.Int(),
                        RejectionReason = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseTask", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApprovalTask", "Id", "dbo.BaseTask");
            DropForeignKey("dbo.BaseTask", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BaseTask", "CreditApplicationId", "dbo.CreditApplication");
            DropIndex("dbo.ApprovalTask", new[] { "Id" });
            DropIndex("dbo.BaseTask", new[] { "CreditApplicationId" });
            DropIndex("dbo.BaseTask", new[] { "UserId" });
            DropTable("dbo.ApprovalTask");
            DropTable("dbo.BaseTask");
        }
    }
}
