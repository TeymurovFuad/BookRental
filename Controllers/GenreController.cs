using BookRental.Models;
using System.Linq;
using System.Web.Mvc;

namespace BookRental.Controllers
{
	public class GenreController : Controller
	{
		ApplicationDbContext db = new ApplicationDbContext();
		// GET: Genre
		public ActionResult Index()
		{
			return View(db.Genres.ToList());
		}

		public ActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Genre genre)
		{
			if (ModelState.IsValid)
			{
				db.Genres.Add(genre);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
		}
	}
}