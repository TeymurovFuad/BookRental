using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BookRental.Models;
using Microsoft.AspNet.Identity;
using BookRental.Utility;
using BookRental.ViewModel;

namespace BookRental.Controllers
{
    public class BookDetailController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookDetail
        public ActionResult Index(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(u => u.Id == userId);

            var bookModel = db.Books.Include(b => b.Genre).SingleOrDefault(b => b.bookIdPK == id);

            var rentalPrice = 0.0;
            var oneMonthRental = 0.0;
            var sixMonthRental = 0.0;
            
            if(user!=null && !User.IsInRole(SD.adminUserRole))
            {
                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes on u.membershipTypeId equals m.membershipTypesIdPK
                                 where u.Id.Equals(userId)
                                 select new
                                 {
                                     m.chargeRateOneMonth,
                                     m.chargeRateSixMonth
                                 };
                oneMonthRental = Convert.ToDouble(bookModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].chargeRateOneMonth) / 100;
                sixMonthRental = Convert.ToDouble(bookModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].chargeRateSixMonth) / 100;

            }
            BookRentalViewModel model = new BookRentalViewModel
            {
                bookId = bookModel.bookIdPK,
                ISBN = bookModel.ISBN,
                author = bookModel.author,
                availability = bookModel.availability,
                dateAdded = bookModel.dateAdded,
                description = bookModel.description,
                Genre = db.Genres.FirstOrDefault(g => g.genreIdPK.Equals(bookModel.genreId)),
                genreId = bookModel.genreId,
                imgUrl = bookModel.imgUrl,
                pages = bookModel.pages,
                Price = bookModel.Price,
                publicationDate = bookModel.publicationDate,
                productDimensions = bookModel.productDimensions,
                tittle = bookModel.tittle,
                userRentId = id,
                rentalPrice = rentalPrice,
                rentalPriceOneMonth = oneMonthRental,
                rentalPriceSixMonth = sixMonthRental,
                publisher = bookModel.publisher
            };

            return View(model);
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