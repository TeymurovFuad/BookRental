using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookRental.Models;

namespace BookRental.Controllers
{
    public class MembershipTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MembershipType
        public ActionResult Index()
        {
            return View(db.MembershipTypes.ToList());
        }

        // GET: MembershipType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipTypes membershipTypes = db.MembershipTypes.Find(id);
            if (membershipTypes == null)
            {
                return HttpNotFound();
            }
            return View(membershipTypes);
        }

        // GET: MembershipType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MembershipType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MembershipTypes membershipTypes)
        {
            if (ModelState.IsValid)
            {
                db.MembershipTypes.Add(membershipTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(membershipTypes);
        }

        // GET: MembershipType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipTypes membershipTypes = db.MembershipTypes.Find(id);
            if (membershipTypes == null)
            {
                return HttpNotFound();
            }
            return View(membershipTypes);
        }

        // POST: MembershipType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MembershipTypes membershipTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membershipTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(membershipTypes);
        }

        // GET: MembershipType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipTypes membershipTypes = db.MembershipTypes.Find(id);
            if (membershipTypes == null)
            {
                return HttpNotFound();
            }
            return View(membershipTypes);
        }

        // POST: MembershipType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MembershipTypes membershipTypes = db.MembershipTypes.Find(id);
            db.MembershipTypes.Remove(membershipTypes);
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
