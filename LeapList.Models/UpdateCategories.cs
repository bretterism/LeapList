using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapList.Models
{
    public class UpdateCategories
    {
        public string Category { get; set; }
        public bool InsertOrDelete { get; set; }
        public string SearchLink { get; set; }
    }
}
