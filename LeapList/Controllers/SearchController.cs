using LeapList.Models;
using LeapList.Search;
using LeapList.DataAccess;
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
        private CLContext db = new CLContext();
        private XmlDocument doc;
        private SearchCriteria searchCriteria;
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchCriteria sc)
        {
            XmlDocument doc = new XmlDocument();
            searchCriteria = sc;

            // TODO: add profile capabilities
            Profile profile = new Profile()
            {
                City = "corvallis"
            };
       
            doc.Load(SearchItems.BuildHttp(sc, profile));

            return View("Result", doc.GetItemList());
        }

        public ActionResult Result(List<CLItem> results)
        {
            return View(results);
        }
    }
}