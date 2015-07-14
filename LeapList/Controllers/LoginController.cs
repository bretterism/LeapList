using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeapList.Models;
using LeapList.DataAccess;

namespace LeapList.Controllers
{
    public class LoginController : Controller
    {
        private CLContext db = new CLContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Login(string username)
        {
            if (ModelState.IsValid)
            {
                Profile profile = db.Profiles.Where(x => x.Username == username).FirstOrDefault();

                var profileData = new UserProfileSessionData
                {
                    ProfileId = profile.ProfileId,
                    City = profile.City,
                    Username = profile.Username
                };

                this.Session["UserProfile"] = profileData;
            }

            return RedirectToAction("Index", "Profile");
        }
    }
}