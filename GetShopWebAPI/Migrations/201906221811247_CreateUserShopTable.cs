namespace GetShopWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserShopTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserShops",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ShopId = c.Int(nullable: false),
                        Like = c.Boolean(nullable: false),
                        Dislike = c.Boolean(nullable: false),
                        ActionDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.UserId, t.ShopId })
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ShopId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserShops", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserShops", "ShopId", "dbo.Shops");
            DropIndex("dbo.UserShops", new[] { "ShopId" });
            DropIndex("dbo.UserShops", new[] { "UserId" });
            DropTable("dbo.UserShops");
        }
    }
}
