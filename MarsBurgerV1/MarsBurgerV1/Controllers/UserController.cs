using MarsBurgerV1.Models;
using MarsBurgerV1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MarsBurgerV1.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db;
        public UserController()
        {
            db = ApplicationDbContext.Create();
        }
        // GET: User
        public ActionResult Index()
        {
            var users = from u in db.Users
                        join m in db.accountTypes on u.AccountTypeId equals m.Id
                        select new UserVM
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email,
                            Phone = u.PhoneNumber,
                            BirthDate = u.BirthDate,
                            AccountTypesId = u.AccountTypeId,
                            AccountTypes = (ICollection<AccountType>)db.accountTypes.ToList().Where(n => n.Id.Equals(u.AccountTypeId)),
                            Disable = u.Disable
                        };
            var usersList = users.ToList();
            return View(usersList);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();
            UserVM uvm = new UserVM
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                AccountTypesId = user.AccountTypeId,
                AccountTypes = db.accountTypes.ToList(),
                Phone = user.PhoneNumber,
                Disable = user.Disable
            };
            return View();
        }
        //
        // Post -> Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserVM user)
        {
            if (!ModelState.IsValid)
            {
                UserVM uvm = new UserVM
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    Email = user.Email,
                    AccountTypesId = user.AccountTypesId,
                    AccountTypes = db.accountTypes.ToList(),
                    Phone = user.Phone,
                    Disable = user.Disable
                };
                return View("Edit", uvm);
            }
            else
            {
                var userInDb = db.Users.Single(u => u.Id == user.Id);
                userInDb.Id = user.Id;
                userInDb.FirstName = user.FirstName;
                userInDb.LastName = user.LastName;
                userInDb.Email = user.Email;
                userInDb.BirthDate = user.BirthDate;
                userInDb.AccountTypeId = user.AccountTypesId;
                userInDb.PhoneNumber = user.Phone;
                userInDb.Disable = user.Disable;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Users");
        }
        public ActionResult Details(string id)
        {
            if (id == null || id.Length == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ApplicationUser user = db.Users.Find(id);
            UserVM uvm = new UserVM
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                AccountTypesId = user.AccountTypeId,
                AccountTypes = db.accountTypes.ToList(),
                Phone = user.PhoneNumber,
                Disable = user.Disable
            };
            return View(uvm);
        }
        //
        //GET: Delete
        public ActionResult Delete(string id)
        {
            if (id == null || id.Length == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ApplicationUser user = db.Users.Find(id);
            UserVM uvm = new UserVM
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                AccountTypesId = user.AccountTypeId,
                AccountTypes = db.accountTypes.ToList(),
                Phone = user.PhoneNumber,
                Disable = user.Disable
            };
            return View(uvm);
        }
        //
        //Post: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var userInDb = db.Users.Find(id);
            if (id == null || id.Length == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            userInDb.Disable = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
        }
    }
}