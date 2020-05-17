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
                    Status = BookRent.statusEnum.Requested,
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
        public ActionResult Index()
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

            if (!User.IsInRole(SD.adminUserRole))
            {
                model = model.Where(u => u.userRentId.Equals(userId));
            }

            return View(model.ToList());
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