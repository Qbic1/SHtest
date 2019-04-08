namespace TestSolutionSmartHead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyDB4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ideas", "Blocked", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "Blocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Blocked");
            DropColumn("dbo.Ideas", "Blocked");
        }
    }
}
