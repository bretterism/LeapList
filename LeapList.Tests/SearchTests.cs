using System;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeapList.Models;
using LeapList.Search;

namespace LeapList.Tests
{
    [TestClass]
    public class SearchTests
    {
        [TestMethod]
        public void TestBuildHttp()
        {
            SearchCriteria sc = new SearchCriteria()
            {
                Category = "ata",
                MaxPrice = 30,
                SearchText = "foobar"
            };

            Profile p = new Profile()
            {
                City = "corvallis",
            };

            string url = SearchItems.BuildHttp(sc, p);

            Assert.AreEqual(@"https://corvallis.craigslist.org/search/ata?max_price=30&query=foobar&format=rss", url);
        }
    }
}
