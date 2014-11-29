namespace AvDB_lab4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientGroups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JuridicalPerson", "ClientGroup", c => c.Int(nullable: false));
            AddColumn("dbo.LegalPerson", "ClientGroup", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LegalPerson", "ClientGroup");
            DropColumn("dbo.JuridicalPerson", "ClientGroup");
        }
    }
}
