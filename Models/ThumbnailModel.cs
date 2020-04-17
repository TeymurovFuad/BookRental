using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
    public class ThumbnailModel
    {
        public int bookId { get; set; }
        public string tittle { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public string imgUrl { get; set; }
        public string link { get; set; }
    }
}