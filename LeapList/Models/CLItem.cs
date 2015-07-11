using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeapList.Models
{
    public class CLItem
    {
        [Key]
        public int ItemId { get; set; }
        public int SearchId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("SearchId")]
        public virtual SearchCriteria SearchCriteria { get; set; }
    }
}