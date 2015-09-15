namespace LeapList.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategorySearch",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        SearchId = c.Int(nullable: false),
                        Category = c.String(nullable: false, maxLength: 3),
                        SearchLink = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.SearchCriteria", t => t.SearchId, cascadeDelete: true)
                .Index(t => t.SearchId);
            
            CreateTable(
                "dbo.SearchCriteria",
                c => new
                    {
                        SearchId = c.Int(nullable: false, identity: true),
                        ProfileId = c.Int(nullable: false),
                        SearchText = c.String(),
                        MinPrice = c.Decimal(precision: 18, scale: 2),
                        MaxPrice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SearchId)
                .ForeignKey("dbo.UserProfile", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Username = c.String(),
                        PasswordHash = c.String(),
                    })
                .PrimaryKey(t => t.ProfileId);
            
            CreateTable(
                "dbo.CLItem",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Link = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SearchCriteria", "ProfileId", "dbo.UserProfile");
            DropForeignKey("dbo.CategorySearch", "SearchId", "dbo.SearchCriteria");
            DropIndex("dbo.SearchCriteria", new[] { "ProfileId" });
            DropIndex("dbo.CategorySearch", new[] { "SearchId" });
            DropTable("dbo.CLItem");
            DropTable("dbo.UserProfile");
            DropTable("dbo.SearchCriteria");
            DropTable("dbo.CategorySearch");
        }
    }
}
