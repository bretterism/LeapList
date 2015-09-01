using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeapList.Models
{
    [Serializable]
    public class UserProfileSessionData
    {
        public int ProfileId { get; set; }
        public string City { get; set; }
        public string Username { get; set; }
        public bool IsPersistent { get; set; }
    }
}