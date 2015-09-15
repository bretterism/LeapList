using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Web.Security;
using LeapList.DataAccess;
using LeapList.Models;
using LeapList.Security;

namespace LeapList.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private CLContext db = new CLContext();
        private UserProfileSessionData profileData;

        // GET: Profile
        public ActionResult Index()
        {
            profileData = AuthCookies.DeserializeCookie<UserProfileSessionData>(HttpContext.Request.Cookies["authenticationToken"]);

            ViewBag.User = profileData.Username;
            List<AddEditSearchVM> searches = Procedures.GetAddEditSearchVMByProfileId(profileData.ProfileId);
            
            return View(searches.GroupBy(g => g.SearchId).Select(s => s.First()).ToList());
        }

        [HttpGet]
        public ActionResult EditSearch(AddEditSearchVM searchVM)
        {
            return PartialView("CategoryModal", searchVM);
        }

        [HttpGet]
        public ActionResult AddSearch()
        {
            AddEditSearchVM vm = new AddEditSearchVM();
            vm.Categories = GetCategories();
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddSearch(AddEditSearchVM vm)
        {
            profileData = AuthCookies.DeserializeCookie<UserProfileSessionData>(HttpContext.Request.Cookies["authenticationToken"]);
            try
            {
                if (ModelState.IsValid)
                {
                    SearchCriteria sc = new SearchCriteria
                    {
                        ProfileId = profileData.ProfileId,
                        SearchText = vm.SearchText,
                        MinPrice = vm.MinPrice,
                        MaxPrice = vm.MaxPrice,
                    };

                    db.AddEntry(sc);

                    List<CategorySearch> catSearch = new List<CategorySearch>();
                    foreach (CheckBoxCategoryVM c in vm.Categories.Where(w => w.IsChecked))
                    {
                        string http = RssPages.BuildHttp(vm, c.Code, profileData.City);
                        catSearch.Add(new CategorySearch { SearchId = sc.SearchId, Category = c.Code, SearchLink = http });
                    }

                    db.AddEntries(catSearch);
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

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
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
    }
}
