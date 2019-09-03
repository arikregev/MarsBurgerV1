using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarsBurgerV1.Models;
using MarsBurgerV1.Utility;
using MarsBurgerV1.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MarsBurgerV1.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: Order
        public ActionResult Index()
        {
            var list = (from o in db.orders
                       join u in db.Users on o.UserId equals u.Id
                       select new OrderVM
                       {
                           UserID = o.UserId,
                           UserName = u.UserName,
                           OrderID = o.Id,
                           CreationTime = o.CreationTime,
                           LastUpdate = o.LastUpdate,
                           OrderStatusID = o.OrderStatusID,
                           Status = o.Status
                       }).ToList();

            string user = User.Identity.GetUserId();
            if (User.IsInRole(SD.AdminUserRole))
            {
                return View(list);
            }
            return View(list.Where(u=>u.UserID.Equals(user)).ToList());
        }
        
        // GET: Order/Details/5
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
      
        // GET: Order/Create
        public ActionResult Create()
        {
            var meals = (from m in db.meals
                         select new ItemVM
                         {
                             Id = m.Id,
                             Name = m.Name,
                             Price = m.Price,
                             Type = SD.ItemType.Meal,
                             ImageURL = m.ImageUrl,
                             Quantity = 0
                         }).ToList();
            var drinks = (from d in db.drinks
                          select new ItemVM
                          {
                              Id = d.Id,
                              Name = d.Name,
                              Price = d.Price,
                              Type = SD.ItemType.Drink,
                              Quantity = 0
                          }).ToList();
            var sidedishes = (from s in db.sidedishes
                              select new ItemVM
                              {
                                  Id = s.Id,
                                  Name = s.Name,
                                  Price = s.Price,
                                  Type = SD.ItemType.SideDish,
                                  Quantity = 0
                              }).ToList();
            var addons = (from a in db.addons
                          select new ItemVM
                          {
                              Id = a.Id,
                              Name = a.Name,
                              Price = a.Price,
                              Type = SD.ItemType.Addon,
                              Quantity = 0
                          }).ToList();
            List<ItemVM> items = new List<ItemVM>();
            items.AddRange(meals);
            items.AddRange(drinks);
            items.AddRange(sidedishes);
            items.AddRange(addons);
            return View(items.ToList());
        }
        // POST: Order/Create
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
                    Order order = new Order
                    {
                        UserId = userID,
                        CreationTime = DateTime.Now,
                        LastUpdate = DateTime.Now,
                        Status = SD.OrderStatus.OrderReceived,
                        OrderStatusID = (int)SD.OrderStatus.OrderReceived
                    };
                    db.orders.Add(order);
                    db.SaveChanges();
                    var lastOrderId = (from o in db.orders
                                       select o).ToList().Last().Id;
                    foreach (var i in items)
                    {
                        OrderItem o = new OrderItem
                        {
                            ItemID = i.Type,
                            ItemTypeId = (int)i.Type,
                            OrderId = lastOrderId
                        };
                        db.OrderItems.Add(o);
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Order/Edit/5
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

        // POST: Order/Edit/5
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

        // GET: Order/Delete/5
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

        // POST: Order/Delete/5
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