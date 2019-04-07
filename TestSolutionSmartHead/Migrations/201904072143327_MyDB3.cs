namespace TestSolutionSmartHead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyDB3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RefreshDate", c => c.DateTime());
            AddColumn("dbo.Users", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "VotesList", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "VotesList");
            DropColumn("dbo.Users", "IsActive");
            DropColumn("dbo.Users", "RefreshDate");
        }
    }
}
