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
        public ActionResult Index(string search = null, bool unclosed = false, string searchOpt = null)
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
                            //OrderStatuses = Enum.GetValues(typeof(SD.OrderStatus)).Cast<SD.OrderStatus>().ToList(),
                            Status = o.Status
                        }).ToList();

            if (unclosed)
            {
                list.RemoveAll(r => r.OrderStatusID == (int)SD.OrderStatus.Closed);
            }
            if (!String.IsNullOrEmpty(search) && searchOpt != null)
            {
                List<OrderVM> filtered = new List<OrderVM>();
                if (searchOpt.Equals(SD.byOrderID))
                {
                    foreach (var i in list)
                    {
                        if (i.OrderID.Equals(Convert.ToInt32(search))&& i.UserID.Equals(User.Identity.GetUserId()))
                        {
                            filtered.Add(i);
                        }
                    }
                    return View(filtered);
                }
                if (searchOpt.Equals(SD.ByOrderStatus))
                {
                    var arr = Enum.GetNames(typeof(SD.OrderStatus)).Where(t=>t.ToLower().Contains(search.ToLower())).ToList();
                    if(arr.Count() > 0)
                    {
                        SD.OrderStatus os = (SD.OrderStatus)Enum.Parse(typeof(SD.OrderStatus), arr.ElementAt(0));
                        int id = (int)os;
                        foreach (var item in list)
                        {
                            if (id == item.OrderStatusID && item.UserID.Equals(User.Identity.GetUserId()))
                                filtered.Add(item);
                        }
                    }
                    return View(filtered);
                }
            }
            string user = User.Identity.GetUserId();
            if (User.IsInRole(SD.AdminUserRole))
            {
                return View(list);
            }
            return View(list.Where(u => u.UserID.Equals(user)).ToList());
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            var uid = db.Users.ToList().Where(m => m.Id.Equals(order.UserId)).Single().UserName;
            OrderVM o = new OrderVM
            {
                UserID = order.UserId,
                UserName = uid,
                OrderID = order.Id,
                CreationTime = order.CreationTime,
                LastUpdate = order.LastUpdate,
                OrderStatusID = order.OrderStatusID,
                //OrderStatuses = Enum.GetValues(typeof(SD.OrderStatus)).Cast<SD.OrderStatus>().ToList(),
                Status = order.Status
            };
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(o);
        }
        public IEnumerable<ItemVM> GetAllItems()
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
            var sidedishes = (from s in db.sidedishes
                              select new ItemVM
                              {
                                  Id = s.Id,
                                  Name = s.Name,
                                  Price = s.Price,
                                  Type = SD.ItemType.SideDish,
                                  ImageURL = s.ImageUrl,
                                  Quantity = 0
                              }).ToList();
            var drinks = (from d in db.drinks
                          select new ItemVM
                          {
                              Id = d.Id,
                              Name = d.Name,
                              Price = d.Price,
                              Type = SD.ItemType.Drink,
                              ImageURL = d.ImageUrl,
                              Quantity = 0
                          }).ToList();
            var addons = (from a in db.addons
                          select new ItemVM
                          {
                              Id = a.Id,
                              Name = a.Name,
                              Price = a.Price,
                              Type = SD.ItemType.Addon,
                              ImageURL = a.ImageUrl,
                              Quantity = 0
                          }).ToList();
            List<ItemVM> items = new List<ItemVM>();
            items.AddRange(meals);
            items.AddRange(drinks);
            items.AddRange(sidedishes);
            items.AddRange(addons);
            return items;
        }
        // GET: Order/Create
        public ActionResult Create()
        {
            return View(GetAllItems().ToList());
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
                if (userID != null)
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
                            OrderId = lastOrderId,
                            Quantity = i.Quantity,
                            ItemOrigID = i.Id
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
            var uid = db.Users.ToList().Where(m => m.Id.Equals(order.UserId)).Single().UserName;
            OrderVM o = new OrderVM
            {
                UserID = order.UserId,
                UserName = uid,
                OrderID = order.Id,
                CreationTime = order.CreationTime,
                LastUpdate = order.LastUpdate,
                OrderStatusID = order.OrderStatusID,
                //OrderStatuses = Enum.GetValues(typeof(SD.OrderStatus)).Cast<SD.OrderStatus>().ToList(),
                Status = order.Status
            };
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(o);
        }
        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderVM order)
        {
            if (ModelState.IsValid)
            {
                var orderInDB = db.orders.Single(u => u.Id == order.OrderID);
                orderInDB.OrderStatusID = order.OrderStatusID;
                orderInDB.Status = order.Status;
                orderInDB.LastUpdate = DateTime.Now;
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
        [ChildActionOnly]
        public PartialViewResult GetOrderedItems(int? orderid, string userid = null)
        {
            if (orderid > 0 && userid != null && db.Users.Any(x => x.Id == userid))
            {
                var allItems = GetAllItems().ToList();
                var uo = db.OrderItems.Where(m => m.OrderId == orderid).ToList();
                foreach (var i in uo)
                {
                    allItems.Find(m => m.Type == (SD.ItemType)i.ItemTypeId && m.Id == i.ItemOrigID).Quantity = i.Quantity;
                }
                allItems.RemoveAll(m => m.Quantity == 0);
                return PartialView("_OrderDetailCreatorPartial", allItems);
            }
            return PartialView();
        }
        public ActionResult Converter()
        {
            return View();
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