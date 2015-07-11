using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeapList.Models
{
    public class SearchCriteria
    {
        [Key]
        public int SearchId { get; set; }
        public int ProfileId { get; set; }
        public string Category { get; set; }
        public string SearchText { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }
        public virtual ICollection<CLItem> CLItems { get; set; }
    }
}