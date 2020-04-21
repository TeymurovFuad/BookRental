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
        public string email { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

        public ICollection<MembershipTypes> MembershipTypes { get; set; }

        [Required]
        public int userMemTypeId { get; set; }

        [Required]
        public string fname { get; set; }

        [Required]
        public string lname { get; set; }

        [Required]
        public string phone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        public DateTime bdate { get; set; }

        public bool disabled { get; set; }


    }
}