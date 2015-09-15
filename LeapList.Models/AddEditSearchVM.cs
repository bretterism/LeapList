using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeapList.Models
{
    public class AddEditSearchVM
    {
        public int SearchId;
        public string SearchText { get; set; }
        public List<CheckBoxCategoryVM> Categories { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public AddEditSearchVM()
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