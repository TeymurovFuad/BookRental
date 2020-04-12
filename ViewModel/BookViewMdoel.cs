using BookRental.Models;
using BookRental.Views.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRental.ViewModel
{
    public class BookViewMdoel
    {
        public IEnumerable<Genre> Genres { get; set; }
    public Book Book { get; set; }
}
}