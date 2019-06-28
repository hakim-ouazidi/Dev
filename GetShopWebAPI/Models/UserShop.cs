using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GetShopWebAPI.Models
{
    public class UserShop
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ShopId { get; set; }

        public ApplicationUser User { get; set; }

        public Shop Shop { get; set; }

        public bool Like { get; set; }

        public bool Dislike { get; set; }

        public DateTime? ActionDate { get; set; }
    }
}