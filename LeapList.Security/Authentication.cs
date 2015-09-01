using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Web.Security;
using LeapList.DataAccess;
using LeapList.Models;

namespace LeapList.Security
{
    public class Authentication
    {

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

    public static class AuthCookies
    {
        public static int SetAuthCookie<T>(this HttpResponseBase response, string name, bool rememberMe, T userData)
        {
            HttpCookie cookie = FormsAuthentication.GetAuthCookie(name, rememberMe);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            var newTicket = new FormsAuthenticationTicket(
                ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration,
                ticket.IsPersistent, serial.Serialize(userData), ticket.CookiePath);

            var encryptTicket = FormsAuthentication.Encrypt(newTicket);

            cookie.Value = encryptTicket;
            response.Cookies.Add(cookie);

            return encryptTicket.Length;
        }

        public static int SetAuthCookie(this HttpResponseBase response, UserProfileSessionData userData)
        {
            HttpCookie cookie = FormsAuthentication.GetAuthCookie(userData.Username, userData.IsPersistent);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            var newTicket = new FormsAuthenticationTicket(
                ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration,
                ticket.IsPersistent, serial.Serialize(userData), ticket.CookiePath);

            var encryptTicket = FormsAuthentication.Encrypt(newTicket);

            cookie.Value = encryptTicket;
            response.Cookies.Add(cookie);

            return encryptTicket.Length;
        }

        public static T DeserializeCookie<T>(HttpCookie cookie)
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            var decryptTicket = FormsAuthentication.Decrypt(cookie.Value);
            return serial.Deserialize<T>(decryptTicket.UserData);
        }
    }
}