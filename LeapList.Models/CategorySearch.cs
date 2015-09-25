using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeapList.Models
{
    public class CategorySearch
    {
        [Key]
        public int CategoryId { get; set; }
        
        [Required]
        public int SearchId { get; set; }
        
        [Required]
        [StringLength(3, MinimumLength=3)]
        public string Category { get; set; }

        [MaxLength(1000)]
        public string SearchLink { get; set; }

        [ForeignKey("SearchId")]
        public virtual SearchCriteria SearchCriteria { get; set; }

    }
}