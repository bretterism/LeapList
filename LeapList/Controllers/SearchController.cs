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
            var results = doc.SearchFor(searchCriteria.SearchText);
            
            return View("Result", results);
        }

        public ActionResult Result(IEnumerable<CLItem> results)
        {
            return View(results.ToList());
        }
    }
}