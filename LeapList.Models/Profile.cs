using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using LeapList.Models;

namespace LeapList.Models
{
    public class Profile
    {
        public Profile() { }

        [Key]
        public int ProfileId { get; set; }
        public string City { get; set; }

        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<SearchCriteria> SearchCriteria { get; set; }
    }
}