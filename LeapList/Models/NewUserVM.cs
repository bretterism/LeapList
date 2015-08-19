using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeapList.Models
{
    public class NewUserVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^.+@.*", ErrorMessage = "Your E-mail must be a valid format.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string City { get; set; }
    }
}