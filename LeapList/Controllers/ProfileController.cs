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
            List<SearchVM> searches = Procedures.GetSearchVMByProfileId(profileData.ProfileId);


            return View(searches.GroupBy(g => g.SearchId).Select(s => s.First()).ToList());
        }

        [HttpGet]
        public ActionResult AddSearch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSearch(SearchVM svm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var profileData = Session["UserProfile"] as UserProfileSessionData;
                    
                    SearchCriteria sc = new SearchCriteria
                    {
                        ProfileId = profileData.ProfileId,
                        SearchText = svm.SearchText,
                        MinPrice = svm.MinPrice,
                        MaxPrice = svm.MaxPrice,
                    };

                    db.AddEntry(sc);
                    
                    List<SC_Category> scc = new List<SC_Category>();
                    foreach (string c in svm.Category)
                    {
                        scc.Add(new SC_Category { SearchId = sc.SearchId, Category = c });
                    }

                    db.AddEntries(scc);
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
