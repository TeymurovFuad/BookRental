using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookRental.Models;
using BookRental.Utility;
using BookRental.ViewModel;
using BookRental.Views.Home;

namespace BookRental.Controllers
{
    [Authorize(Roles = SD.adminUserRole)]
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Book
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Genre);
            return View(books.ToList());
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            var mdoel = new BookViewMdoel
            {
                Book = book,
                Genres = db.Genres.ToList()
            };
            return View(mdoel);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var genre = db.Genres.ToList();
            var mdoel = new BookViewMdoel
            {
                Genres = genre
            };
            return View(mdoel);
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookViewMdoel bookVM)
        {
            var book = new Book
            {
                author = bookVM.Book.author,
                availability = bookVM.Book.availability,
                ISBN = Convert.ToString(DateTimeOffset.Now.ToUnixTimeSeconds()),
                Price = bookVM.Book.Price,
                publicationDate = bookVM.Book.publicationDate,
                dateAdded = bookVM.Book.dateAdded,
                pages = bookVM.Book.pages,
                Genre = bookVM.Book.Genre,
                genreId = bookVM.Book.genreId,
                description = bookVM.Book.description,
                imgUrl = bookVM.Book.imgUrl,
                productDimensions = bookVM.Book.productDimensions,
                tittle = bookVM.Book.tittle,
                publisher = bookVM.Book.publisher
            };

            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            bookVM.Genres = db.Genres.ToList();
            return View(bookVM);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            var model = new BookViewMdoel
            {
                Book = book,
                Genres = db.Genres.ToList()
            };
            return View(model);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(BookViewMdoel bookVM)
        {
            var book = new Book
            {
                author = bookVM.Book.author,
                availability = bookVM.Book.availability,
                ISBN = Convert.ToString(DateTimeOffset.Now.ToUnixTimeSeconds()),
                Price = bookVM.Book.Price,
                publicationDate = bookVM.Book.publicationDate,
                dateAdded = bookVM.Book.dateAdded,
                pages = bookVM.Book.pages,
                Genre = bookVM.Book.Genre,
                genreId = bookVM.Book.genreId,
                description = bookVM.Book.description,
                imgUrl = bookVM.Book.imgUrl,
                productDimensions = bookVM.Book.productDimensions,
                tittle = bookVM.Book.tittle,
                bookIdPK = bookVM.Book.bookIdPK,
                publisher = bookVM.Book.publisher
            };
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            bookVM.Genres = db.Genres.ToList();
            return View(bookVM);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            var model = new BookViewMdoel
            {
                Book = book,
                Genres = db.Genres.ToList()
            };
            return View(model);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
