using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeapList.Models
{
    public class SearchCriteria
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Category { get; set; }
        public string SearchText { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

    }
}