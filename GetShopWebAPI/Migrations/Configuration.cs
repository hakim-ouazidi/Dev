namespace GetShopWebAPI.Migrations
{
    using GetShopWebAPI.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GetShopWebAPI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GetShopWebAPI.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            IList<Shop> ListOfShops = new List<Shop>();

            ListOfShops.Add(new Shop() { Name="Cake Shop",Distance=120,Adresse="14-16 Bury Pl, Holborn" });
            ListOfShops.Add(new Shop() { Name="Art Shop",Distance=235,Adresse="132 Finchley Rd, NW3 5HS"  });
            ListOfShops.Add(new Shop() { Name="The Monocle Shop",Distance=60,Adresse="2 George St, Marylebone,W1U 3QS"  });
            ListOfShops.Add(new Shop() { Name="National Gallery Shop",Distance=300,Adresse="64 Exhibition Rd, South Kensington"  });
            ListOfShops.Add(new Shop() { Name="The Conran Shop",Distance=400,Adresse="55 Marylebone High St, Marylebone, W1U 5HS"  });
            ListOfShops.Add(new Shop() { Name="Element Store",Distance=95,Adresse="13 Short's Gardens,  WC2H 9AT"  });
            context.Shops.AddRange(ListOfShops);

            base.Seed(context);
           
        }
    }
}
