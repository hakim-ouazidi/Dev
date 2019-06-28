namespace GetShopWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdresseToShopTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shops", "Adresse", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shops", "Adresse");
        }
    }
}
