using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Xml;

namespace LeapList.Models
{
    public class CLItem
    {
        [Key]
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CategorySearch CategorySearches { get; set; }
    }
}