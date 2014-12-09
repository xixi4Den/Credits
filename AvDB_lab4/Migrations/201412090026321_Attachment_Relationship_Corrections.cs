namespace AvDB_lab4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Attachment_Relationship_Corrections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attachment", "CreditApplication_Id", "dbo.CreditApplication");
            DropIndex("dbo.Attachment", new[] { "CreditApplication_Id" });
            DropColumn("dbo.Attachment", "ApplicationId");
            RenameColumn(table: "dbo.Attachment", name: "CreditApplication_Id", newName: "ApplicationId");
            AlterColumn("dbo.Attachment", "ApplicationId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Attachment", "ApplicationId");
            AddForeignKey("dbo.Attachment", "ApplicationId", "dbo.CreditApplication", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachment", "ApplicationId", "dbo.CreditApplication");
            DropIndex("dbo.Attachment", new[] { "ApplicationId" });
            AlterColumn("dbo.Attachment", "ApplicationId", c => c.Guid());
            RenameColumn(table: "dbo.Attachment", name: "ApplicationId", newName: "CreditApplication_Id");
            AddColumn("dbo.Attachment", "ApplicationId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Attachment", "CreditApplication_Id");
            AddForeignKey("dbo.Attachment", "CreditApplication_Id", "dbo.CreditApplication", "Id");
        }
    }
}
