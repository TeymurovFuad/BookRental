using BookRental.Models;
using BookRental.Utility;
using BookRental.ViewModel;
using BookRental.Views.Home;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;

namespace BookRental.Controllers
{
    public class BookRentController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //Get: Create
        public ActionResult Create(string tittle = null, string ISBN = null)
        {
            if (tittle != null & ISBN != null)
            {
                BookRentalViewModel model = new BookRentalViewModel
                {
                    tittle = tittle,
                    ISBN = ISBN
                };
            }
            return View();
        }

        //Post: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookRentalViewModel bookRent)
        {
            if (ModelState.IsValid)
            {
                var email = bookRent.email;

                var userDetails = from u in db.Users
                                  where (email.Equals(email))
                                  select new
                                  {
                                      u.Id
                                  };

                var ISBN = bookRent.ISBN;

                Book bookSelected = db.Books.Where(b => b.ISBN == (ISBN)).FirstOrDefault();

                var rentalDuration = bookRent.rentalDuration;

                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes on u.membershipTypeId equals m.membershipTypesIdPK
                                 where u.Email.Equals(email)
                                 select new
                                 {
                                     m.chargeRateOneMonth,
                                     m.chargeRateSixMonth
                                 };

                var oneMonthRental = Convert.ToDouble(bookSelected.Price) * Convert.ToDouble(chargeRate.ToList()[0].chargeRateOneMonth) / 100;
                var sixMonthRental = Convert.ToDouble(bookSelected.Price) * Convert.ToDouble(chargeRate.ToList()[0].chargeRateSixMonth) / 100;

                double rentalPrice = 0;

                if (bookRent.rentalDuration == SD.sixMonthCount)
                {
                    rentalPrice = sixMonthRental;
                }
                else
                {
                    rentalPrice = oneMonthRental;
                }

                BookRent ModelToAddToDB = new BookRent
                {
                    bookRentId = bookSelected.bookIdPK,
                    rentalPrice = rentalPrice,
                    scheduledEndDate = bookRent.scheduledEndDate,
                    rentalDuration = bookRent.rentalDuration,
                    Status = BookRent.statusEnum.approved,
                    userRentId = userDetails.ToList()[0].Id
                };

                bookSelected.availability -= 1;
                db.BookRents.Add(ModelToAddToDB);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }

        // GET: BookRent
        public ActionResult Index(int? pageNumber, string option = null, string search = null)
        {
            string userId = User.Identity.GetUserId();

            var model = from br in db.BookRents
                        join b in db.Books on br.bookRentId equals b.bookIdPK
                        join u in db.Users on br.userRentId equals u.Id
                        select new BookRentalViewModel
                        {
                            bookId = b.bookIdPK,
                            rentalPrice = br.rentalPrice,
                            Price = b.Price,
                            pages = b.pages,
                            fname = u.fname,
                            lname = u.lname,
                            bdate = u.bdate,
                            scheduledEndDate = br.scheduledEndDate,
                            author = b.author,
                            availability = b.availability,
                            dateAdded = b.dateAdded,
                            description = b.description,
                            email = u.Email,
                            genreId = b.genreId,
                            Genre = db.Genres.Where(g => g.genreIdPK.Equals(b.genreId)).FirstOrDefault(),
                            ISBN = b.imgUrl,
                            productDimensions = b.productDimensions,
                            publisher = b.publisher,
                            rentalDuration = br.rentalDuration,
                            Status = br.Status.ToString(),
                            tittle = b.tittle,
                            userRentId = u.Id,
                            bookRentalIdPK = br.bookRentIdPK,
                            startDate = br.startDate
                        };

            if (option == "email" && search.Length > 0)
            {
                model = model.Where(u => u.email.Contains(search));
            }
            if (option == "name" && search.Length > 0)
            {
                model = model.Where(u => u.fname.Contains(search) || u.lname.Contains(search));
            }
            if (option == "status" && search.Length > 0)
            {
                model = model.Where(u => u.Status.Contains(search));
            }

            if (!User.IsInRole(SD.adminUserRole))
            {
                model = model.Where(u => u.userRentId.Equals(userId));
            }

            return View(model.ToList().ToPagedList(pageNumber ?? 1, 10));
        }

        [HttpPost]
        public ActionResult Reserve(BookRentalViewModel book)
        {
            var userId = User.Identity.GetUserId();
            Book bookToRent = db.Books.Find(book.bookId);
            double rentalPrice = 0;
            if (userId != null)
            {
                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes on u.membershipTypeId equals m.membershipTypesIdPK
                                 where u.Id.Equals(userId)
                                 select new
                                 {
                                     m.chargeRateOneMonth,
                                     m.chargeRateSixMonth
                                 };

                if (book.rentalDuration == SD.sixMonthCount)
                {
                    rentalPrice = Convert.ToDouble(bookToRent.Price) * Convert.ToDouble(chargeRate.ToList()[0].chargeRateSixMonth) / 100;
                }
                else
                {
                    rentalPrice = Convert.ToDouble(bookToRent.Price) * Convert.ToDouble(chargeRate.ToList()[0].chargeRateOneMonth) / 100;
                }
                BookRent bookRent = new BookRent
                {
                    bookRentId = bookToRent.bookIdPK,
                    userRentId = userId,
                    rentalDuration = book.rentalDuration,
                    rentalPrice = rentalPrice,
                    Status = BookRent.statusEnum.Requested
                };

                db.BookRents.Add(bookRent);

                var bookIdDb = db.Books.SingleOrDefault(c => c.bookIdPK == book.bookId);
                bookIdDb.availability -= 1;
                db.SaveChanges();
                return RedirectToAction("Index", "BookRent");
            }

            return View();
        }


        //Details: Get
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookRent bookRent = db.BookRents.Find(id);

            var model = getViewModelFromBookRent(bookRent);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }


        //Decline: Get
        public ActionResult Decline(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookRent bookRent = db.BookRents.Find(id);

            var model = getViewModelFromBookRent(bookRent);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        //Decline: Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Decline(BookRentalViewModel model, int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookRent bookRent = db.BookRents.Find(id);
            bookRent.Status = BookRent.statusEnum.Rejected;

            Book bookInDB = db.Books.Find(bookRent.bookRentId);
            bookInDB.availability += 1;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //Approve: Get
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookRent bookRent = db.BookRents.Find(id);

            var model = getViewModelFromBookRent(bookRent);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View("Approve", model);
        }

        //Approve: Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(BookRentalViewModel model, int? id)
        {
            if (id == 0 || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookRent bookRent = db.BookRents.Find(id);
            bookRent.Status = BookRent.statusEnum.approved;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //PickUp: Get
        public ActionResult PickUp(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookRent bookRent = db.BookRents.Find(id);

            var model = getViewModelFromBookRent(bookRent);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View("Approve", model);
        }

        //PickUp: Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PickUp(BookRentalViewModel model, int? id)
        {
            if (id == 0 || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookRent bookRent = db.BookRents.Find(id);
            bookRent.Status = BookRent.statusEnum.Rented;
            bookRent.startDate = Convert.ToDateTime(DateTime.Now.ToString("MMM dd yyyy HH:mm"));
            if (bookRent.rentalDuration == SD.sixMonthCount)
            {
                bookRent.scheduledEndDate = DateTime.Now.AddMonths(Convert.ToInt32(SD.sixMonthCount));
            }
            else
            {
                bookRent.scheduledEndDate = DateTime.Now.AddMonths(Convert.ToInt32(SD.oneMonthCount));
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //Return: Get
        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookRent bookRent = db.BookRents.Find(id);

            var model = getViewModelFromBookRent(bookRent);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View("Approve", model);
        }

        //Return: Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(BookRentalViewModel model, int? id)
        {
            if (id == 0 || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookRent bookRent = db.BookRents.Find(id);
            bookRent.Status = BookRent.statusEnum.Closed;

            bookRent.additionalCharge = model.additionalCharge;

            Book bookInDB = db.Books.Find(bookRent.bookRentId);

            bookInDB.availability += 1;

            bookRent.actualEndDate = Convert.ToDateTime("02/02/2021 05:05");

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        private BookRentalViewModel getViewModelFromBookRent(BookRent bookRent)
        {
            Book bookSelected = db.Books.Where(b => b.bookIdPK == bookRent.bookRentId).FirstOrDefault();

            var userDetails = from u in db.Users
                              where u.Id.Equals(bookRent.userRentId)
                              select new
                              {
                                  u.Id,
                                  u.fname,
                                  u.lname,
                                  u.bdate,
                                  u.Email
                              };

            BookRentalViewModel model = new BookRentalViewModel()
            {
                userRentId = bookRent.userRentId,
                bookRentalIdPK = bookSelected.bookIdPK,
                rentalPrice = bookRent.rentalPrice,
                Price = bookSelected.Price,
                pages = bookSelected.pages,
                fname = userDetails.ToList()[0].fname,
                lname = userDetails.ToList()[0].lname,
                bdate = userDetails.ToList()[0].bdate,
                email = userDetails.ToList()[0].Email,
                scheduledEndDate = bookRent.scheduledEndDate,
                author = bookSelected.author,
                startDate = bookRent.startDate,
                availability = bookSelected.availability,
                description = bookSelected.description,
                genreId = bookSelected.genreId,
                Genre = db.Genres.FirstOrDefault(g => g.genreIdPK.Equals(bookSelected.genreId)),
                ISBN = bookSelected.ISBN,
                imgUrl = bookSelected.imgUrl,
                productDimensions = bookSelected.productDimensions,
                publicationDate = bookSelected.publicationDate,
                publisher = bookSelected.publisher,
                Status = bookRent.Status.ToString(),
                tittle = bookSelected.tittle,
                additionalCharge = bookRent.additionalCharge
            };

            return model;
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