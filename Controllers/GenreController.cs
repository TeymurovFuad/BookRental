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

		public ActionResult Details(int? id)
		{
			if(id == null)
				{
					return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
				}
			Genre genre = db.Genres.Find(id);
			if(genre == null)
			{
				return HttpNotFound();
			}
			return View(genre);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
		}
	}
}