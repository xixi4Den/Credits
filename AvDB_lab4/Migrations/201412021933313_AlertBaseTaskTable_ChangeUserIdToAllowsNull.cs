namespace AvDB_lab4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlertBaseTaskTable_ChangeUserIdToAllowsNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BaseTask", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.BaseTask", new[] { "UserId" });
            AlterColumn("dbo.BaseTask", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.BaseTask", "UserId");
            AddForeignKey("dbo.BaseTask", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BaseTask", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.BaseTask", new[] { "UserId" });
            AlterColumn("dbo.BaseTask", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.BaseTask", "UserId");
            AddForeignKey("dbo.BaseTask", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
