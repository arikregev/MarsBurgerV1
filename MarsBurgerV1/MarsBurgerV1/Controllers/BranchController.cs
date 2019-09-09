using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MarsBurgerV1.Models;
using MarsBurgerV1.Utility;

namespace MarsBurgerV1.Controllers
{
    public class BranchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Branch
        public ActionResult Index(string search = null, bool Accessible = false, string searchOpt = null)
        {
            var list = db.Branches.ToList();
            if (Accessible)
            {
                list.RemoveAll(t => t.AccessibleBranch == false);
            }
            if(!String.IsNullOrEmpty(search) && searchOpt != null)
            {
                if (searchOpt.Equals(SD.byBranchName))
                {
                    return View(list.Where(t => t.Name.ToLower().Contains(search.ToLower())));
                }
                else if (searchOpt.Equals(SD.byCityName))
                {
                    return View(list.Where(t => t.City.ToLower().Contains(search.ToLower())));
                }
            }
            return View(list);
        }

        // GET: Branch/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branches branches = db.Branches.Find(id);
            if (branches == null)
            {
                return HttpNotFound();
            }
            return View(branches);
        }
        [Authorize]
        // GET: Branch/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Branch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,PhoneNumber,City,UserId")] Branches branches)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branches);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branches);
        }
        [Authorize]
        // GET: Branch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branches branches = db.Branches.Find(id);
            if (branches == null)
            {
                return HttpNotFound();
            }
            return View(branches);
        }

        // POST: Branch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,PhoneNumber,City,UserId")] Branches branches)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branches).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branches);
        }
        [Authorize]
        // GET: Branch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branches branches = db.Branches.Find(id);
            if (branches == null)
            {
                return HttpNotFound();
            }
            return View(branches);
        }

        // POST: Branch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branches branches = db.Branches.Find(id);
            db.Branches.Remove(branches);
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
