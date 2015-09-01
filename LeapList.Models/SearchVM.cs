using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LeapList.Models
{
    public class SearchVM
    {
        public SearchVM()
        {
            Category = new List<string>();
        }

        public int SearchId { get; set; }
        public string SearchText { get; set; }

        public List<string> Category { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }
    }
}