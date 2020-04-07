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
        [Key]
        public int genreIdPK { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }
    }
};