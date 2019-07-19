 using System;

namespace AquaMarket.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; } // current page number
        public int PageSize { get; set; } // count of objects on the page
        public int TotalItems { get; set; } // total objects count
        public int TotalPages  // total pages count
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
}