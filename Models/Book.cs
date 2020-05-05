using BookRental.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookRental.Views.Home
{
    public class Book
    {
        [Required]
        [Key]
        public int bookIdPK{ get; set; }


        [Required]
        public string ISBN { get; set; }


        [Required]
        [Display(Name = "Tittle")]
        public string tittle { get; set; }


        [Required]
        [Display(Name = "Author")]
        public string author { get; set; }


        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }


        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string imgUrl { get; set; }


        [Required]
        [Range(0,1000)]
        [Display(Name = "In Stock")]
        public int availability { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public double Price { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0: MMM/dd/yyyy}")]
        [Display(Name = "Added")]
        public DateTime? dateAdded { get; set; }


        [Required]
        [Display(Name = "Genre ID")]
        public int genreId { get; set; }


        public Genre Genre { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MMM dd yyyy}")]
        [Display(Name = "Published")]
        public DateTime publicationDate { get; set; }


        [Required]
        [Display(Name = "Pages")]
        public int pages { get; set; }


        [Required]
        [Display(Name = "Dimensions")]
        public string productDimensions { get; set; }


        [Required]
        [Display(Name = "Publisher")]
        public string publisher { get; set; }
    }
}