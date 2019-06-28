namespace GetShopWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteEnCascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserShops", "ShopId", "dbo.Shops");
            AddColumn("dbo.Shops", "UserShop_UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Shops", "UserShop_ShopId", c => c.Int());
            CreateIndex("dbo.Shops", new[] { "UserShop_UserId", "UserShop_ShopId" });
            AddForeignKey("dbo.Shops", new[] { "UserShop_UserId", "UserShop_ShopId" }, "dbo.UserShops", new[] { "UserId", "ShopId" });
            AddForeignKey("dbo.UserShops", "ShopId", "dbo.Shops", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserShops", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.Shops", new[] { "UserShop_UserId", "UserShop_ShopId" }, "dbo.UserShops");
            DropIndex("dbo.Shops", new[] { "UserShop_UserId", "UserShop_ShopId" });
            DropColumn("dbo.Shops", "UserShop_ShopId");
            DropColumn("dbo.Shops", "UserShop_UserId");
            AddForeignKey("dbo.UserShops", "ShopId", "dbo.Shops", "Id", cascadeDelete: true);
        }
    }
}
