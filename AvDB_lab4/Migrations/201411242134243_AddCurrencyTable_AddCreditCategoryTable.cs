namespace AvDB_lab4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrencyTable_AddCreditCategoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Currency",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(nullable: false),
                        Abbreviation = c.String(nullable: false, maxLength: 3),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CreditCategory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 5),
                        DisplayText = c.String(nullable: false),
                        Span = c.Int(nullable: false),
                        Rate = c.Double(nullable: false),
                        RepaymentScheme = c.Int(nullable: false),
                        IsEarlyRepayment = c.Boolean(nullable: false),
                        MaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClientGroup = c.Int(nullable: false),
                        CurrencyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currency", t => t.CurrencyId, cascadeDelete: true)
                .Index(t => t.CurrencyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditCategory", "CurrencyId", "dbo.Currency");
            DropIndex("dbo.CreditCategory", new[] { "CurrencyId" });
            DropTable("dbo.CreditCategory");
            DropTable("dbo.Currency");
        }
    }
}
