using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRental.Extensions
{
    public static class ThumbnailExtension
    {
        public static IEnumerable<ThumbnailModel> GetBookThumbnail(this List<ThumbnailModel> thumbnails, ApplicationDbContext db)
        {
            try
            {
                if (db == null)
                {
                    db = ApplicationDbContext.Create();
                }

                thumbnails = (from b in db.Books
                              select new ThumbnailModel
                              {
                                  bookId = b.bookIdPK,
                                  tittle = b.tittle,
                                  description = b.description,
                                  imgUrl = b.imgUrl,
                                  link = "/BookDetail/Index/" + b.bookIdPK
                              }).ToList();
            }
            catch (Exception ex)
            {

            }
            return thumbnails.OrderBy(b => b.tittle);
        }
    }
}