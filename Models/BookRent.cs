using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
    public class BookRent
    {
        //ID
        [Key]
        [Required]
        public int bookRentIdPK { get; set; }

        //User ID
        [Required]
        public int userRentId { get; set; }

        //Book ID
        [Required]
        public int bookRentId { get; set; }

        //Rent Start Dage
        [Display(Name = "Start Date")]
        public DateTime? startDate { get; set; }

        //Actual End Dage
        [Display(Name = "Actual End Date")]
        public DateTime? actualEndDate { get; set; }

        //Scheduled End Dage
        [Display(Name = "Scheduled End Date")]
        public DateTime? scheduledEndDate { get; set; }

        //Additional Charge
        [Display(Name = "Additional Charge")]
        public bool? additionalCharge { get; set; }

        //Rental Price
        [Display(Name = "Rental Price")]
        [Required]
        public bool rentalPrice { get; set; }

        //Actual status of rental
        [Display(Name = "Rental Duration")]
        [Required]
        public string rentalDuration { get; set; }

        //Actual status of rental
        [Display(Name = "Rental Status")]
        [Required]
        public statusEnum Status { get; set; }

        public enum statusEnum
        {
            Requested,
            approved,
            Rejected,
            Rented,
            Closed
        }
    }
}