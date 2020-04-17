using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRental.Extensions
{
    public static class ThumbnailExtension
    {
        public static IEnumerable<ThumbnailModel> GetBookThumbnail(this List<ThumbnailModel> thumbnails, ApplicationDbContext db = null, string search = null)
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
                                  author = b.author,
                                  description = b.description,
                                  imgUrl = b.imgUrl,
                                  link = "/BookDetail/Index/" + b.bookIdPK,
                              }).ToList();

                if (search != null)
                {
                    return thumbnails.Where(t => t.tittle.ToLower().Contains(search.ToLower())).OrderBy(t => t.tittle);
                }
            }
            catch (Exception ex)
            {

            }
            return thumbnails.OrderBy(b => b.tittle);
        }
    }
}