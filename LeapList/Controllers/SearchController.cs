using LeapList.Models;
using LeapList.Parse;
using LeapList.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace LeapList.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index(string searchText)
        {
            return View();
        }
    }
}