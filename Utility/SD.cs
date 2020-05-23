using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRental.Utility
{
    public static class SD
    {
        //static details

        public const string endUserRole = "Customer";
        public const string adminUserRole = "Super Admin";

        public const string oneMonth = "One Month";
        public const string sixMonth = "Six Month";

        public const string oneMonthCount = "1";
        public const string sixMonthCount = "6";

        public const string Requested = "requested";
        public const string Approved = "approved";
        public const string Rented = "rented";
        public const string PickedUp = "pickedup";
        public const string Closed = "closed";
    }
}