namespace TestSolutionSmartHead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyDB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 30));
            CreateIndex("dbo.Users", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Name" });
            AlterColumn("dbo.Users", "Name", c => c.String());
        }
    }
}
