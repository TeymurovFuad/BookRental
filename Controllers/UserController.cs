using BookRental.Models;
using BookRental.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookRental.Controllers
{
    public class UserController : Controller
    {

        private ApplicationDbContext db;

        public UserController()
        {
            db = ApplicationDbContext.Create();
        }



        // GET: User/Index
        public ActionResult Index()
        {
            var userList = (from u in db.Users
                       join m in db.MembershipTypes on u.membershipTypeId equals m.membershipTypesIdPK
                       select new UserViewModel
                       {
                           Id = u.Id,
                           fname = u.fname,
                           lname = u.lname,
                           email = u.Email,
                           phone = u.phone,
                           bdate = u.bdate,
                           userMemTypeId = u.membershipTypeId,
                           //MembershipTypes = (ICollection<MembershipTypes>)db.MembershipTypes.ToList().Where(n => n.membershipTypesIdPK.Equals(u.membershipTypeId)),
                           disabled = u.disabled

                       }).ToList();

            return View(userList);
        }


        //Get: User/Edit
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            UserViewModel model = new UserViewModel
            {
                fname = user.fname,
                lname = user.lname,
                email = user.Email,
                phone = user.phone,
                bdate = user.bdate,
                Id = user.Id,
                userMemTypeId = user.membershipTypeId,
                MembershipTypes = db.MembershipTypes.ToList(),
                disabled = user.disabled
            };
            return View(model);
        }

        //Post: User/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                UserViewModel model = new UserViewModel
                {
                    fname = user.fname,
                    lname = user.lname,
                    email = user.email,
                    phone = user.phone,
                    bdate = user.bdate,
                    Id = user.Id,
                    userMemTypeId = user.userMemTypeId,
                    MembershipTypes = db.MembershipTypes.ToList(),
                    disabled = user.disabled
                };
                return View("Edit", "User");
            }
            else
            {
                var userInDB = db.Users.Single(u => u.Id == user.Id);

                userInDB.fname = user.fname;
                userInDB.lname = user.lname;
                userInDB.Email = user.email;
                userInDB.phone = user.phone;
                userInDB.bdate = user.bdate;
                userInDB.membershipTypeId = user.userMemTypeId;
                userInDB.disabled = user.disabled;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "User");
        }

        //Get: User/Details
        public ActionResult Details(string id)
        {
            if (id == null || id.Length == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = db.Users.Find(id);

            UserViewModel model = new UserViewModel
            {
                fname = user.fname,
                lname = user.lname,
                email = user.Email,
                phone = user.phone,
                bdate = user.bdate,
                userMemTypeId = user.membershipTypeId,
                MembershipTypes = db.MembershipTypes.ToList(),
                disabled = user.disabled
            };
            return View(model);
        }

        //Get: User/Delete
        public ActionResult Delete(string id)
        {
            if (id == null || id.Length == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = db.Users.Find(id);

            UserViewModel model = new UserViewModel
            {
                fname = user.fname,
                lname = user.lname,
                email = user.Email,
                phone = user.phone,
                bdate = user.bdate,
                userMemTypeId = user.membershipTypeId,
                MembershipTypes = db.MembershipTypes.ToList(),
                disabled = user.disabled
            };
            return View(model);
        }


        //Post: User/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(string id)
        {
            var userInDB = db.Users.Find(id);
            if (id == null || id.Length == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            userInDB.disabled = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}