using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeapList.Models;
using LeapList.DataAccess;
using System.Web.Security;

namespace LeapList.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private CLContext db = new CLContext();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (!Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Profile");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public RedirectToRouteResult Login(Login user)
        {
            if (ModelState.IsValid)
            {
                if (Authentication.ValidateUser(user.Username, user.Password))
                {
                    Profile profile = db.Profiles.Where(x => x.Username == user.Username).FirstOrDefault();

                    var profileData = new UserProfileSessionData
                    {
                        ProfileId = profile.ProfileId,
                        City = profile.City,
                        Username = profile.Username
                    };

                    Response.SetAuthCookie(user.Username, user.RememberMe, profileData);

                    if (profileData.City == "N/A")
                    {
                        return RedirectToAction("SelectCity");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Profile");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The Username and Password were incorrect.");
                }
            }
            return RedirectToAction("Login");

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult NewUser(NewUserVM vm)
        {
            if (ModelState.IsValid)
            {
                if (Procedures.CheckIfUserExists(vm.Username.ToLower()))
                {
                    ModelState.AddModelError("UserExists", 
                        string.Format("The Username {0} already exists. Please select another username.", vm.Username));

                    return View(vm);
                }

                Profile profile = new Profile(vm.Username, vm.Password, "N/A");

                db.AddEntry(profile);
                CreateCookie(profile, false);

                return RedirectToAction("SelectCity");
            }
            
            return View(vm);
        }

        [HttpGet]
        public ActionResult SelectCity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SelectCity(string city)
        {
            // get the cookie
            UserProfileSessionData profileData =
                AuthCookies.DeserializeCookie<UserProfileSessionData>(HttpContext.Request.Cookies["authenticationToken"]);

            // modify the profile's city in db
            profileData.City = city;
            db.UpdateCityForProfile(profileData);

            // overwrite cookie with new value
            Response.SetAuthCookie(profileData);

            return RedirectToAction("Index", "Profile");
        }

        private void CreateCookie(Profile profile, bool rememberMe)
        {
            var profileData = new UserProfileSessionData
            {
                ProfileId = profile.ProfileId,
                City = profile.City,
                Username = profile.Username,
                IsPersistent = rememberMe
            };

            Response.SetAuthCookie(profile.Username, rememberMe, profileData);
        }
    }
}