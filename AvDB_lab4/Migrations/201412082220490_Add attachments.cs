namespace AvDB_lab4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addattachments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Source = c.String(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                        IsContract = c.Boolean(nullable: false),
                        CreditApplication_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CreditApplication", t => t.CreditApplication_Id)
                .Index(t => t.CreditApplication_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachment", "CreditApplication_Id", "dbo.CreditApplication");
            DropIndex("dbo.Attachment", new[] { "CreditApplication_Id" });
            DropTable("dbo.Attachment");
        }
    }
}
