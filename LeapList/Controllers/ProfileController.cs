using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeapList.DataAccess;
using LeapList.Models;

namespace LeapList.Controllers
{
    public class ProfileController : Controller
    {
        private CLContext db = new CLContext();

        // GET: Profile
        public ActionResult Index()
        {
            var profileData = Session["UserProfile"] as UserProfileSessionData;
            
            ViewBag.User = profileData.Username;

            return View(db.SearchCriteria
                .Where(x => x.ProfileId == profileData.ProfileId)
                .ToList()); 
        }
    }
}
