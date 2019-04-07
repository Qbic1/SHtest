namespace TestSolutionSmartHead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ideas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ball = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Text = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Password = c.String(),
                        Votes = c.Byte(nullable: false),
                        Role = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ideas", "User_Id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Name" });
            DropIndex("dbo.Ideas", new[] { "User_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Ideas");
        }
    }
}
