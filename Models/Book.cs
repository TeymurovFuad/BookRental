using BookRental.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookRental.Views.Home
{
    public class Book
    {
        [Required]
        public int bookIdPK{ get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string tittle { get; set; }
        [Required]
        public string author { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string imgUrl { get; set; }
        [Required]
        [Range(0,1000)]
        public string availability { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double currency { get; set; }
        [Required]
        [DisplayFormat(DataFormatString ="{0: mm dd yyyy}")]
        public DateTime? dataAdded { get; set; }
        [Required]
        public int genreId { get; set; }

        public Genre Genre { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0: mm dd yyyy}")]
        public DateTime publicationDate { get; set; }

        [Required]
        public int pages { get; set; }
        [Required]
        public string productDimensions { get; set; }
    }
}