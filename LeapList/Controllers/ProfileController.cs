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
        [Authorize]
        public ActionResult AddSearch()
        {
            AddSearchVM vm = new AddSearchVM();
            vm.Categories = GetCategories();
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddSearch(AddSearchVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var profileData = Session["UserProfile"] as UserProfileSessionData;
                    
                    SearchCriteria sc = new SearchCriteria
                    {
                        ProfileId = profileData.ProfileId,
                        SearchText = vm.SearchText,
                        MinPrice = vm.MinPrice,
                        MaxPrice = vm.MaxPrice,
                    };

                    db.AddEntry(sc);
                    
                    List<SC_Category> scc = new List<SC_Category>();
                    foreach (CheckBoxCategoryVM c in vm.Categories.Where(w => w.IsChecked))
                    {
                        scc.Add(new SC_Category { SearchId = sc.SearchId, Category = c.Code });
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

        public static List<CheckBoxCategoryVM> GetCategories()
        {
            List<CheckBoxCategoryVM> categories = new List<CheckBoxCategoryVM>();

            foreach (KeyValuePair<string, string> d in DictCategory.GetCategories())
            {
                categories.Add(new CheckBoxCategoryVM
                {
                    Code = d.Key,
                    Name = d.Value,
                });
            }
            return categories;
        }

        public void UpdateCategories(string[] selectedCategories, AddSearchVM vm)
        {

        }
    }
}
