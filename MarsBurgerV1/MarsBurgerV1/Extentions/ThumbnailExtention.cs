using MarsBurgerV1.Models;
using MarsBurgerV1.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.Extentions
{
    public static class ThumbnailExtention
    {
        public static IEnumerable<Thumbnail> GetMealThumbnail(this List<Thumbnail> thumbnails, ApplicationDbContext db = null, string search = null)
        {
            try
            {
                if (db == null)
                {
                    db = ApplicationDbContext.Create();
                }
                char[] toTrim = { '~', '/' };

                thumbnails = (from m in db.meals
                              select new Thumbnail
                              {
                                  MealId = m.Id,
                                  MealName = m.Name,
                                  Description = m.Description,
                                  ImageUrl = m.ImageUrl,
                                  Link = "/Meal/Details/" + m.Id
                              }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Catch from TE");
            }
            foreach (var item in thumbnails)
            {
                item.SetImageUrlAndTrim(item.ImageUrl);
            }
            if (search != null)
                return thumbnails.Where(t => t.MealName.ToLower().Contains(search.ToLower())).OrderBy(b => b.MealName);
            if (HttpContext.Current.Request.Cookies.Get("interest") != null)
            {
                string intrest = HttpContext.Current.Request.Cookies.Get("interest").Value.ToString();
                ThComparer th = new ThComparer(intrest);
                thumbnails.Sort(th);
                thumbnails.Reverse();
                return thumbnails;

            }
            return thumbnails.OrderBy(b => b.MealName);
        }
    }
}