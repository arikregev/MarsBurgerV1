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
    [Authorize(Roles = SD.AdminUserRole)]
    public class AddonController : Controller
    {
        private ApplicationDbContext db;
        public AddonController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Addon
        public ActionResult Index()
        {
            return View(db.addons.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ImageUrl,Price")] Addon addon)
        {
            if (ModelState.IsValid)
            {
                if (!addon.ImageUrl.Contains("~/"))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("~/Media/").Append(addon.ImageUrl).Append(".jpg");
                    addon.ImageUrl = sb.ToString();
                }
                db.addons.Add(addon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(addon);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Addon addon = db.addons.Find(id);
            if (addon == null)
            {
                return HttpNotFound();
            }
            return View(addon);
        }
        // GET: Addon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Addon addon = db.addons.Find(id);
            if (addon == null)
            {
                return HttpNotFound();
            }
            return View(addon);
        }

        // POST: Addon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price")] Addon addon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addon);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Addon addon = db.addons.Find(id);
            if (addon == null)
            {
                return HttpNotFound();
            }
            return View(addon);
        }

        // POST: Addon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Addon addon = db.addons.Find(id);
            db.addons.Remove(addon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}