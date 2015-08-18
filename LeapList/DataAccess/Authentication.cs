using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using LeapList.DataAccess;
using LeapList.Models;

namespace LeapList.DataAccess
{
    public class Authentication
    {
        CLContext db = new CLContext();

        public static string GetHash(string toHash)
        {
            return BitConverter.ToString(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(toHash))).Replace("-", string.Empty);
        }

        public static bool ValidateUser(string usernameEntered, string passwordEntered)
        {
            UsernamePassword user = Procedures.GetUsernameAndPasswordHash(usernameEntered);

            if (string.Equals(GetHash(passwordEntered), user.PasswordHash))
            {
                return true;
            }
            return false;
            
        }
    }
}