namespace LeapList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CLItem",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        SearchId = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Link = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
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
                .ForeignKey("dbo.Profile", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.ProfileId);
            
            CreateTable(
                "dbo.SC_Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        SearchId = c.Int(nullable: false),
                        Category = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.SearchCriteria", t => t.SearchId, cascadeDelete: true)
                .Index(t => t.SearchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SC_Category", "SearchId", "dbo.SearchCriteria");
            DropForeignKey("dbo.SearchCriteria", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.CLItem", "SearchId", "dbo.SearchCriteria");
            DropIndex("dbo.SC_Category", new[] { "SearchId" });
            DropIndex("dbo.SearchCriteria", new[] { "ProfileId" });
            DropIndex("dbo.CLItem", new[] { "SearchId" });
            DropTable("dbo.SC_Category");
            DropTable("dbo.Profile");
            DropTable("dbo.SearchCriteria");
            DropTable("dbo.CLItem");
        }
    }
}
