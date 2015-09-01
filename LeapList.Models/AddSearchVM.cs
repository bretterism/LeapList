using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeapList.Models
{
    public class AddSearchVM
    {
        public string SearchText { get; set; }
        public List<CheckBoxCategoryVM> Categories { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public AddSearchVM()
        {
            Categories = new List<CheckBoxCategoryVM>();
        }
    }

    public class CheckBoxCategoryVM
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsChecked { get; set; }
    }
}