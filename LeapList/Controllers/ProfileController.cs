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
        public ActionResult EditSearch(int? searchId)
        {
            if (searchId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.AddEdit = "Edit";

            AddEditSearchVM svm = Procedures.GetAddEditSearchVMBySearchId(searchId);
            List<CheckBoxCategoryVM> checkBoxes = DictCategory.GetCheckBoxCategoryVMList();
            foreach (CheckBoxCategoryVM checkBox in svm.Categories)
            {
                checkBoxes.Single(s => s.Code == checkBox.Code).IsChecked = true;
            }
            svm.Categories = checkBoxes;

            return View("AddEditSearch", svm);
        }

        [HttpPost]
        public ActionResult EditSearch(AddEditSearchVM vm, AddEditSearchVM oldState, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    profileData = AuthCookies.DeserializeCookie<UserProfileSessionData>(HttpContext.Request.Cookies["authenticationToken"]);
                    AddEditSearchVM previousState = Procedures.GetAddEditSearchVMBySearchId(id);
                    
                    List<UpdateCategories> updateCategories = new List<UpdateCategories>();
                    foreach (CheckBoxCategoryVM cat in vm.Categories)
                    {
                        CheckBoxCategoryVM previousCat = new CheckBoxCategoryVM();
                        previousCat = previousState.Categories.FirstOrDefault(s => s.Code == cat.Code) ?? null;

                        if (cat.IsChecked && previousCat == null)
                        {
                            string http = RssPages.BuildHttp(vm, cat.Code, profileData.City);
                            updateCategories.Add(new UpdateCategories()
                            {
                                Category = cat.Code,
                                InsertOrDelete = cat.IsChecked,
                                SearchLink = http
                            });
                        }

                        if (previousCat != null && previousCat.IsChecked && !cat.IsChecked)
                        {
                            updateCategories.Add(new UpdateCategories()
                            {
                                Category = previousCat.Code,
                                InsertOrDelete = false
                            });
                        }
                    }

                    Procedures.UpdateSearchCriteria(id,
                        vm.SearchText,
                        vm.MinPrice ?? default(decimal),
                        vm.MaxPrice ?? default(decimal),
                        updateCategories);
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Please try again. If the same error keeps occurring, try again another time.");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddSearch()
        {
            ViewBag.AddEdit = "Add";
            AddEditSearchVM vm = new AddEditSearchVM();
            vm.Categories = GetCategories();
            return View("AddEditSearch", vm);
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
