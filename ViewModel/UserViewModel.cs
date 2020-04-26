using BookRental.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRental.ViewModel
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

        public ICollection<MembershipTypes> MembershipTypes { get; set; }

        [Required]
        [Display(Name = "Membership Type")]
        public int userMemTypeId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string fname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lname { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string phone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        [Display(Name = "Birth Date")]
        public DateTime bdate { get; set; }

        [Display(Name = "Disabled")]
        public bool disabled { get; set; }


    }
}