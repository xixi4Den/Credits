namespace AvDB_lab4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreditApplication : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseClient",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClientGroup = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CreditApplication",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                        CompleteDate = c.DateTime(),
                        ClientId = c.Guid(nullable: false),
                        CreditCategoryId = c.Guid(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        Outcome = c.Int(),
                        RejectionReason = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseClient", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.CreditCategory", t => t.CreditCategoryId)
                .Index(t => t.ClientId)
                .Index(t => t.CreditCategoryId);

            AddForeignKey("dbo.LegalPerson", "Id", "dbo.BaseClient", "Id");
            AddForeignKey("dbo.JuridicalPerson", "Id", "dbo.BaseClient", "Id");
            CreateIndex("dbo.LegalPerson", "Id");
            CreateIndex("dbo.JuridicalPerson", "Id"); 
            DropColumn("dbo.JuridicalPerson", "ClientGroup");
            DropColumn("dbo.LegalPerson", "ClientGroup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LegalPerson", "ClientGroup", c => c.Int(nullable: false));
            AddColumn("dbo.JuridicalPerson", "ClientGroup", c => c.Int(nullable: false));
            DropForeignKey("dbo.JuridicalPerson", "Id", "dbo.BaseClient");
            DropForeignKey("dbo.LegalPerson", "Id", "dbo.BaseClient");
            DropForeignKey("dbo.CreditApplication", "CreditCategoryId", "dbo.CreditCategory");
            DropForeignKey("dbo.CreditApplication", "ClientId", "dbo.BaseClient");
            DropIndex("dbo.JuridicalPerson", new[] { "Id" });
            DropIndex("dbo.LegalPerson", new[] { "Id" });
            DropIndex("dbo.CreditApplication", new[] { "CreditCategoryId" });
            DropIndex("dbo.CreditApplication", new[] { "ClientId" });
            DropTable("dbo.CreditApplication");
            DropTable("dbo.BaseClient");
        }
    }
}
