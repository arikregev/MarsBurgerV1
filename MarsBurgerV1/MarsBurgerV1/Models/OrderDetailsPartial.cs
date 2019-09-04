using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.Models
{
    public class OrderDetailsPartial
    {
        public int? ItemId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string ImageUrl { get; set; }
        public int? Quantity { get; set; }
    }
}