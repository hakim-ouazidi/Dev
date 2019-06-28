using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetShopWebAPI.Dtos
{
   public class ShopDTO
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adresse { get; set; }

        public decimal Distance { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool Like { get; set; }

        public bool Dislike { get; set; }

    }
}