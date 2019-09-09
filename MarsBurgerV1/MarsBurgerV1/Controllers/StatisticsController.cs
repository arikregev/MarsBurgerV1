using MarsBurgerV1.Models;
using MarsBurgerV1.Utility;
using MarsBurgerV1.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MarsBurgerV1.Controllers
{
    public class StatisticsController : Controller
    {
        private ApplicationDbContext db;
        public StatisticsController(){

            db = new ApplicationDbContext();
        }

        // GET: Statistics
        public ActionResult Index()
        {
            var data = (from o in db.OrderItems
                        join m in db.meals on o.ItemOrigID equals m.Id
                        where o.ItemTypeId == (int)SD.ItemType.Meal
                        select new ItemVM
                        {
                            Id = m.Id,
                            Name = m.Name,
                            ImageURL = m.ImageUrl,
                            Price = m.Price,
                            Type = SD.ItemType.Meal,
                            Quantity = o.Quantity
                        }).ToList();
            ViewBag.Data = data;
            return View(data);
        }
        public JsonResult GetMealsData()
        {
            var data = (from o in db.OrderItems
                        join m in db.meals on o.ItemOrigID equals m.Id
                        where o.ItemTypeId == (int)SD.ItemType.Meal
                        select new ItemVM
                        {
                            Id = m.Id,
                            Name = m.Name,
                            ImageURL = m.ImageUrl,
                            Price = m.Price,
                            Type = SD.ItemType.Meal,
                            Quantity = o.Quantity
                        }).ToList();
            return this.Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}