using MarsBurgerV1.Models;
using MarsBurgerV1.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MarsBurgerV1.Controllers
{
    [Authorize(Roles = SD.AdminUserRole)]
    public class SideDishController : Controller
    {
        private ApplicationDbContext db;
        public SideDishController()
        {
            db = new ApplicationDbContext();
        }
        // GET: SideDish
        public ActionResult Index()
        {
            return View(db.sidedishes.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price")] SideDish sd)
        {
            if (ModelState.IsValid)
            {
                db.sidedishes.Add(sd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sd);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SideDish sd = db.sidedishes.Find(id);
            if (sd == null)
            {
                return HttpNotFound();
            }
            return View(sd);
        }
        // GET: SideDish/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SideDish sd = db.sidedishes.Find(id);
            if (sd == null)
            {
                return HttpNotFound();
            }
            return View(sd);
        }

        // POST: SideDish/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price")] SideDish sd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sd);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SideDish sd = db.sidedishes.Find(id);
            if (sd == null)
            {
                return HttpNotFound();
            }
            return View(sd);
        }

        // POST: SideDish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SideDish sd = db.sidedishes.Find(id);
            db.sidedishes.Remove(sd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}