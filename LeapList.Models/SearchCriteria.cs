﻿using System;
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
        
        [Display(Name="Search")]
        public string SearchText { get; set; }

        [Display(Name="Min Price")]
        public decimal? MinPrice { get; set; }

        [Display(Name = "Max Price")]
        public decimal? MaxPrice { get; set; }

        [ForeignKey("ProfileId")]
        public virtual UserProfile UserProfile { get; set; }
        
        public virtual ICollection<CategorySearch> CategorySearches { get; set; }
    }
}