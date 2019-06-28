using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetShopWebAPI.Models
{
    public class Shop
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adresse { get; set; }

        public decimal Distance { get; set; }
    }
}