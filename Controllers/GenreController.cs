using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookRental.Controllers
{
    public class GenreController : Controller
    {
        private DataContext db;
        public GenreController()
        {
            db = new DataContext();
        }
        // GET: Genre
        public ActionResult Index()
        {
            return View(db.Genres.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}