namespace TestSolutionSmartHead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyDB1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ideas", "Positive", c => c.Int(nullable: false));
            AddColumn("dbo.Ideas", "Negative", c => c.Int(nullable: false));
            DropColumn("dbo.Ideas", "Ball");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ideas", "Ball", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Ideas", "Negative");
            DropColumn("dbo.Ideas", "Positive");
        }
    }
}
