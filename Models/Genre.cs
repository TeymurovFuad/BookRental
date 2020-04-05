using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
    public class Genre
    {
        [Required]
        public int genreIdPK { get; set; }
        [Required]
        public string name { get; set; }
    }
}