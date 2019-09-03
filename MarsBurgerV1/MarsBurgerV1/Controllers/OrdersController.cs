using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarsBurgerV1.Models;
using MarsBurgerV1.ViewModel;

namespace MarsBurgerV1.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Orders
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        public ActionResult AddMoreItemsViews(int? offset, int? count)
        {
            return PartialView(new OrderItem());
        }
        // GET: Orders/Create
        public ActionResult Create()
        {
            var meals = (from m in db.meals
                         select new ItemVM
                         {
                             Id = m.Id,
                             Name = m.Name,
                             Price = m.Price,
                             ItemType = "Meal",
                             ImageURL = m.ImageUrl,
                             Quantity = 0
                         }).ToList();
            var drinks = (from d in db.drinks
                          select new ItemVM
                          {
                              Id = d.Id,
                              Name = d.Name,
                              Price = d.Price,
                              ItemType = "Drink",
                              Quantity = 0
                          }).ToList();
            var sidedishes = (from s in db.sidedishes
                              select new ItemVM
                              {
                                  Id = s.Id,
                                  Name = s.Name,
                                  Price = s.Price,
                                  ItemType = "SideDish",
                                  Quantity = 0
                              }).ToList();
            var addons = (from a in db.addons
                          select new ItemVM
                          {
                              Id = a.Id,
                              Name = a.Name,
                              Price = a.Price,
                              ItemType = "Addon",
                              Quantity = 0
                          }).ToList();
            List<ItemVM> items = new List<ItemVM>();
            items.AddRange(meals);
            items.AddRange(drinks);
            items.AddRange(sidedishes);
            items.AddRange(addons);
            int _id = 1;
            foreach (var i in items) { i.Id = _id++; }
            return View(items.ToList());
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IEnumerable<ItemVM> items = null)
        {
            if (ModelState.IsValid)
            {
                //db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastUpdate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.orders.Find(id);
            db.orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
