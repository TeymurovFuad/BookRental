using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookRental.Controllers.API
{
    public class UsersAPIController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //To retrieve Email or (Name & Birthdate)
        public IHttpActionResult Get(string type, string query = null)
      {
            if(type.Equals("email") && query != null)
            {
                var customerQuery = db.Users.Where(u => u.Email.ToLower().Contains(query.ToLower()));

                return Ok(customerQuery.ToList());
            }

            if (type.Equals("name") && query != null)
            {
                var customerQuery = from u in db.Users
                                    where u.Email.Contains(query)
                                    select new
                                    {
                                        u.fname,
                                        u.lname,
                                        u.bdate
                                    };

                return Ok(customerQuery.ToList()[0].fname + " " + customerQuery.ToList()[0].lname + ";" + customerQuery.ToList()[0].bdate);
            }
            return BadRequest();
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
