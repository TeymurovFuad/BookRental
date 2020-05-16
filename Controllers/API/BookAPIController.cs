using BookRental.Models;
using BookRental.Utility;
using BookRental.Views.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookRental.Controllers.API
{
    public class BookAPIController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //Tittle
        public IHttpActionResult Get(string query = null)
       {
            var bookQuery = db.Books.Where(b => b.tittle.ToLower().Contains(query.ToLower()));
            return Ok(bookQuery.ToList());
        }

        //Price or Availability (price/avail)
        public IHttpActionResult Get(string type, string ISBN = null, string rentalDuration = null, string email = null)
        {
            if (type.Equals("price"))
            {
                Book BookQuery = db.Books.Where(b => b.ISBN.Equals(ISBN)).SingleOrDefault();

                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes on u.membershipTypeId equals m.membershipTypesIdPK
                                 where u.Email.Equals(email)
                                 select new
                                 {
                                     m.chargeRateOneMonth,
                                     m.chargeRateSixMonth
                                 };

                var price = Convert.ToDouble(BookQuery.Price) * Convert.ToDouble(chargeRate.ToList()[0].chargeRateOneMonth) / 100;

                if (rentalDuration == SD.sixMonthCount)
                {
                    price = Convert.ToDouble(BookQuery.Price) * Convert.ToDouble(chargeRate.ToList()[0].chargeRateSixMonth) / 100;
                }
                return Ok(price);
            }
            else
            {
                Book BookQuery = db.Books.Where(b => b.ISBN.Equals(ISBN)).SingleOrDefault();

                return Ok(BookQuery.availability);
            }
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
