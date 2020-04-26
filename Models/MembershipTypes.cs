using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
    public class MembershipTypes
    {
        [Required]
        [Key]
        public int membershipTypesIdPK { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Sign Up Fee")]
        public int SignUpFee { get; set; }

        [Required]
        [Display(Name = "Charge Rate For A Month")]

        public byte chargeRateOneMonth { get; set; }

        [Required]
        [Display(Name = "Charge Rate For 6 months")]
        public byte chargeRateSixMonth { get; set; }
    }
}