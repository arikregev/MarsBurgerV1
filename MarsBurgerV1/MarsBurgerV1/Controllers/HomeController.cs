using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarsBurgerV1.ViewModel;
using MarsBurgerV1.Extentions;
using MarsBurgerV1.Models;

namespace MarsBurgerV1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string search = null)
        {
            var thumbnails = new List<Thumbnail>().GetMealThumbnail(ApplicationDbContext.Create(), search);
            var count = thumbnails.Count() / 4;
            var model = new List<ThumbnailBoxVM>();
            for (int i = 0; i <= count; i++)
            {
                model.Add(new ThumbnailBoxVM
                {
                    Thumbnails = thumbnails.Skip(i * 4).Take(4)
                });
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult RemoteData(string query)
        {
            List<string> listData = null;
            if (!string.IsNullOrEmpty(query))
            {
                using(var db = ApplicationDbContext.Create())
                {
                    listData = (from m in db.meals select m.Name).Where(q=>q.ToLower().Contains(query.ToLower())).ToList(); 
                }
            }
            return Json(new { Data = listData });
        }
    }
}