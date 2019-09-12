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
            var itemVMs = (from o in db.OrderItems
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
            var Items = itemVMs.GroupBy(l => l.Id).Select(cl => new
            {
                name = cl.First().Name,
                quan = cl.Sum(c => c.Quantity)
            }).ToList();
            var Side = (from o in db.OrderItems join s in db.sidedishes
                         on o.ItemOrigID equals s.Id
                         where o.ItemTypeId == (int)SD.ItemType.SideDish
                         select new
                         {
                             name = s.Name,
                             quantity = o.Quantity
                         }).ToList();
            var SideG = Side.GroupBy(l => l.name)
                .Select(cl => new
                {
                    name = cl.First().name,
                    quan = cl.Sum(c=>c.quantity)
                }).ToList();
            List<string> namesOfItems = new List<string>();
            List<int> quantityOfItems = new List<int>();
            List<string> SideNames = new List<string>();
            List<int> SideQun = new List<int>();
            foreach (var i in Items)
            {
                namesOfItems.Add(i.name);
                quantityOfItems.Add(i.quan);
            }
            foreach (var s in SideG)
            {
                SideNames.Add(s.name);
                SideQun.Add(s.quan);
            }
            ViewBag.namesOfItems = namesOfItems;
            ViewBag.quantityOfItems = quantityOfItems;
            ViewBag.sideNames = SideNames;
            ViewBag.sideQun = SideQun;
            return View();
        }
    }
}