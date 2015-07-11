using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeapList.Models
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }
        public string City { get; set; }

        // TODO: Uername/Password authentication.
        public string Username { get; set; }
    }
}