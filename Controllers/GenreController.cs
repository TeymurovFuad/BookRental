using BookRental.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

		//Post: Create
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

		//Get: Details
		public ActionResult Details(int? id)
		{
			if(id == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			Genre genre = db.Genres.Find(id);
			if(genre == null)
			{
				return HttpNotFound();
			}
			return View(genre);
		}

		//Get: Edit
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Genre genre = db.Genres.Find(id);
			if (genre == null)
			{
				return HttpNotFound();
			}
			return View(genre);
		}

		//Post: Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Genre genre)
		{
			if (ModelState.IsValid)
			{
				//var genreId = db.Genres.FirstOrDefault(n => n.genreIdPK.Equals(genre.genreIdPK));
				//genreId.name = genre.name;
				db.Entry(genre).State = EntityState.Modified;
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