using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.Models
{
    public class Thumbnail
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }


        public Thumbnail SetImageUrlAndTrim(string url)
        {
            ImageUrl = url.TrimStart('~','/');
            return this;
        }
    }
}