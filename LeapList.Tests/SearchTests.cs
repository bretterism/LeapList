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
            SearchCriteria sc = new SearchCriteria
            {
                MaxPrice = 30,
                SearchText = "foobar",
                SearchId = 123
            };

            SearchVM svm = new SearchVM
            {
                Category = "ata",
                SearchText = "foobar",
                MaxPrice = 800,
                MinPrice = 300
            };

            Profile p = new Profile
            {
                City = "corvallis",
            };

            string url = SearchItems.BuildHttp(svm, p);

            Assert.AreEqual(@"http://corvallis.craigslist.org/search/ata?min_price=300&max_price=800&query=foobar&format=rss", url);
        }
    }
}
