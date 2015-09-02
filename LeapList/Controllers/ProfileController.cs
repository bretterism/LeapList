﻿using System;
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
            List<SearchVM> searches = Procedures.GetSearchVMByProfileId(profileData.ProfileId);
            foreach (SearchVM search in searches)
            {
                for (int i = 0; i < search.Category.Count; i++)
                {
                    search.Category[i] = DictCategory.GetCategoryName(search.Category[i]);
                }
            }
            return View(searches.GroupBy(g => g.SearchId).Select(s => s.First()).ToList());
        }

        [HttpGet]
        public ActionResult AddSearch()
        {
            AddSearchVM vm = new AddSearchVM();
            vm.Categories = GetCategories();
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddSearch(AddSearchVM vm)
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

                    SearchVM searchVM = new SearchVM()
                    {
                        SearchText = vm.SearchText,
                        MinPrice = vm.MinPrice,
                        MaxPrice = vm.MaxPrice
                    };

                    foreach (CheckBoxCategoryVM checkbox in vm.Categories)
                    {
                        if (checkbox.IsChecked)
                        {
                            searchVM.Category.Add(checkbox.Code);
                        }
                    }

                    
                    List<CategorySearch> catSearch = new List<CategorySearch>();
                    foreach (CheckBoxCategoryVM c in vm.Categories.Where(w => w.IsChecked))
                    {
                        string http = RssPages.BuildHttp(searchVM, c.Code, profileData.City);
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

        public void UpdateCategories(string[] selectedCategories, AddSearchVM vm)
        {

        }
    }
}
