using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeapList.Models;
using LeapList.Parse;
using System.Xml;

namespace LeapList.Controllers
{
    public class ListController : Controller
    {
        public ActionResult Index()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"https://corvallis.craigslist.org/search/bka?format=rss");
            
            // ViewBag.Items = List<CLItems>
            ViewBag.Items = doc.GetItemList();
            return View();
        }
    }
}