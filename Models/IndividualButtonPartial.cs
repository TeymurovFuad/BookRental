using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BookRental.Models
{
    public class IndividualButtonPartial
    {
        public string buttonType { get; set; }
        public string action { get; set; }
        public string icon { get; set; }
        public string text { get; set; }


        public int? genreId { get; set; }
        public int? bookId { get; set; }
        public int? customerId { get; set; }
        public int? membershipTypeId { get; set; }
        public string userId { get; set; }
        public int? bookRentalId { get; set; }

        public string ActionParameter
        {
            get
            {
                var param = new StringBuilder(@"/");
                if(genreId != null && genreId > 0)
                {
                    param.Append(string.Format("{0}", genreId));
                }
                if (bookId != null && bookId > 0)
                {
                    param.Append(string.Format("{0}", bookId));
                }
                if (customerId != null && customerId > 0)
                {
                    param.Append(string.Format("{0}", customerId));
                }
                if (membershipTypeId != null && membershipTypeId > 0)
                {
                    param.Append(string.Format("{0}", membershipTypeId));
                }
                if (userId != null && userId.Trim().Length > 0)
                {
                    param.Append(string.Format("{0}", userId));
                }
                if (bookRentalId != null && bookRentalId > 0)
                {
                    param.Append(string.Format("{0}", bookRentalId));
                }

                return param.ToString();
            }
        }
    }
}