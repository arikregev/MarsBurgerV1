using MarsBurgerV1.Models;
using MarsBurgerV1.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MarsBurgerV1.Controllers
{
    public class MealController : Controller
    {
        private ApplicationDbContext db;
        public MealController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Meal
        [Authorize(Roles = SD.AdminUserRole)]
        public ActionResult Index()
        {
            var count = (from m in db.meals.GroupBy(p => p.Id) select new { count = m.Count() }).ToList();
            ViewBag.countMeals = count.Count();
            return View(db.meals.ToList());
        }
        [Authorize(Roles = SD.AdminUserRole)]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = SD.AdminUserRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ImageUrl,Price")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("~/Media/").Append(meal.ImageUrl).Append(".jpg");
                meal.ImageUrl = sb.ToString();
                db.meals.Add(meal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meal);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }
        // GET: Meal/Edit/5
        [Authorize(Roles = SD.AdminUserRole)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // POST: Meal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = SD.AdminUserRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ImageUrl,Price")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                if (!meal.ImageUrl.First().Equals('~'))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("~/Media/").Append(meal.ImageUrl).Append(".jpg");
                    meal.ImageUrl = sb.ToString();
                }
                db.Entry(meal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meal);
        }
        [Authorize(Roles = SD.AdminUserRole)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // POST: Meal/Delete/5
        [Authorize(Roles = SD.AdminUserRole)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meal meal = db.meals.Find(id);
            db.meals.Remove(meal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}