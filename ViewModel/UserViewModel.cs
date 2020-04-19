using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BookRental.Models;
using BookRental.ViewModel;

namespace BookRental.Models
{
    public class UserViewModel
    {
        [Required]
        [Key]
        public int userIdPK { get; set; }

        [Required]
        [Display(Name = "Email Adress")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        public string consfimrPassword { get; set; }

        public ICollection<MembershipTypes> MembershipTypes { get; set; }

        [Required]
        public int userMemTypeId { get; set; }

        [Required]
        public string fname { get; set; }

        [Required]
        public string lname { get; set; }

        [Required]
        public string phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        public DateTime bdate { get; set; }

        public bool disabled { get; set; }
    }
}