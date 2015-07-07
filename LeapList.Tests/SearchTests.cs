using System;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeapList.Models;
using LeapList.Parse;
using LeapList.Search;

namespace LeapList.Tests
{
    [TestClass]
    public class SearchTests
    {
        [TestMethod]
        public void TestSearchFor()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"../../Content/TestCLFeed.xml");

            List<CLItem> items = new List<CLItem>();
            items = doc.GetItemList();

            IEnumerable<CLItem> filter = doc.SearchFor("Anatomy");
            CLItem item = filter.ToList().FirstOrDefault();

            Assert.IsNotNull(item);
            Assert.AreEqual(item.Title, "Anatomy and Physiology Books $80");
        }
    }
}
