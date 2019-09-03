using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MarsBurgerV1.Models;
using MarsBurgerV1.ViewModel;
using Microsoft.AspNet.Identity;

namespace MarsBurgerV1.Controllers
{
    public class OrderController : Controller
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
      
        // GET: Orders/Create
        public ActionResult Create()
        {
            var meals = (from m in db.meals
                         select new ItemVM
                         {
                             Id = m.Id,
                             Name = m.Name,
                             Price = m.Price,
                             Type = ItemVM.ItemType.Meal,
                             ImageURL = m.ImageUrl,
                             Quantity = 0
                         }).ToList();
            var drinks = (from d in db.drinks
                          select new ItemVM
                          {
                              Id = d.Id,
                              Name = d.Name,
                              Price = d.Price,
                              Type = ItemVM.ItemType.Drink,
                              Quantity = 0
                          }).ToList();
            var sidedishes = (from s in db.sidedishes
                              select new ItemVM
                              {
                                  Id = s.Id,
                                  Name = s.Name,
                                  Price = s.Price,
                                  Type = ItemVM.ItemType.SideDish,
                                  Quantity = 0
                              }).ToList();
            var addons = (from a in db.addons
                          select new ItemVM
                          {
                              Id = a.Id,
                              Name = a.Name,
                              Price = a.Price,
                              Type = ItemVM.ItemType.Addon,
                              Quantity = 0
                          }).ToList();
            List<ItemVM> items = new List<ItemVM>();
            items.AddRange(meals);
            items.AddRange(drinks);
            items.AddRange(sidedishes);
            items.AddRange(addons);
            return View(items.ToList());
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<ItemVM> items = null)
        {
            if (ModelState.IsValid)
            {
                var userID = User.Identity.GetUserId();
                items.RemoveAll(m => m.Quantity.Equals(0));
                if(userID != null)
                {

                }
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