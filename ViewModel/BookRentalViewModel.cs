﻿using BookRental.Models;
using BookRental.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static BookRental.Models.BookRent;

namespace BookRental.ViewModel
{
    public class BookRentalViewModel
    {
        //BOOK DETAILS

        [Key]
        public int bookRentalIdPK { get; set; }

        public int bookId { get; set; }


        public string ISBN { get; set; }


        [Display(Name = "Tittle")]
        public string tittle { get; set; }


        [Display(Name = "Author")]
        public string author { get; set; }


        [Display(Name = "Description")]
        public string description { get; set; }


        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string imgUrl { get; set; }


        [Range(0, 1000)]
        [Display(Name = "In Stock")]
        public int availability { get; set; }


        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public double Price { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MMM/dd/yyyy}")]
        [Display(Name = "Added")]
        public DateTime? dateAdded { get; set; }


        [Display(Name = "Genre ID")]
        public int genreId { get; set; }


        public Genre Genre { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MMM dd yyyy}")]
        [Display(Name = "Published")]
        public DateTime publicationDate { get; set; }


        [Display(Name = "Pages")]
        public int pages { get; set; }


        [Display(Name = "Dimensions")]
        public string productDimensions { get; set; }


        [Display(Name = "Publisher")]
        public string publisher { get; set; }


        //RENTAL DETAILS


        //Rent Start Dage
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MMM dd yyyy}")]
        public DateTime? startDate { get; set; }

        //Actual End Dage
        [Display(Name = "Actual End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MMM dd yyyy}")]
        public DateTime? actualEndDate { get; set; }

        //Scheduled End Dage
        [Display(Name = "Scheduled End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MMM dd yyyy}")]
        public DateTime? scheduledEndDate { get; set; }

        //Additional Charge
        [Display(Name = "Additional Charge")]
        public double? additionalCharge { get; set; }

        //Rental Price
        [Display(Name = "Price")]
        public double rentalPrice { get; set; }

        //Actual status of rental
        [Display(Name = "Duration")]
        public string rentalDuration { get; set; }

        //Actual status of rental
        [Display(Name = "Status")]
        public string Status { get; set; }

        //One Month Price
        [Display(Name = "One Month Price")]
        public double rentalPriceOneMonth { get; set; }

        //Six Month Price
        [Display(Name = "Six Month Price")]
        public double rentalPriceSixMonth { get; set; }


        //USER DETAILS

        //User ID
        public string userRentId { get; set; }

        //User Email
        [Display(Name = "Email")]
        public string email { get; set; }

        //First Name
        [Display(Name = "First Name")]
        public string fname { get; set; }

        //User Last Name
        [Display(Name = "Last Name")]
        public string lname { get; set; }

        //User Full Name
        public string completeName { get { return fname + " " + lname; } }

        //User Birth Date
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MMM dd yyyy}")]
        [Display(Name = "Birdth Date")]
        public DateTime bdate { get; set; }

        public string actionName
        {
            get
            {
                if (Status.ToLower().Contains(SD.Requested))
                {
                    return "Approve";
                }
                if (Status.ToLower().Contains(SD.Approved))
                {
                    return "PickUp";
                }
                if (Status.ToLower().Contains(SD.Rented))
                {
                    return "Return";
                }
                return null;
            }
        }
    }
}