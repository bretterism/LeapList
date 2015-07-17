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
using System.Data.Entity.Infrastructure;

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

        [HttpGet]
        public ActionResult AddSearch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSearch(
            [Bind(Include="Category, SearchText, MinPrice, MaxPrice")] 
            SearchCriteria sc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var profileData = Session["UserProfile"] as UserProfileSessionData;
                    sc.ProfileId = profileData.ProfileId;

                    db.AddEntry(sc);
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Please try again. If the same error keeps occurring, try again another time.");
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSearch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var profileData = Session["UserProfile"] as UserProfileSessionData;
            SearchCriteria sc = db.SearchCriteria.Find(id);
            if (sc == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.DeleteEntry(sc);
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Please try again. If the same error keeps occurring, try again another time.");
            }

            return RedirectToAction("Index");
        }
    }
}
