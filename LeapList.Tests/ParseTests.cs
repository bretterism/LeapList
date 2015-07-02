using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Collections.Generic;
using LeapList.Models;
using LeapList.Parse;

namespace LeapList.Tests
{
    [TestClass]
    public class ParseTests
    {
        [TestMethod]
        public void TestGetItemList()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"../../Content/TestCLFeed.xml");

            List<CLItem> items = new List<CLItem>();
            items = doc.GetItemList();

            Assert.AreEqual(items[0].Title, "Attention Book Collectors. GONE WITH THE WIND 2nd Edition (Albany OR) $40");
            Assert.AreEqual(items[0].Price, 40);
            Assert.AreEqual(items[0].Link, @"http://corvallis.craigslist.org/bkd/5102618758.html");
            Assert.AreEqual(items[0].Description, "We Have A November 1936 Printing 2nd Edition Of Gone With The Wind. In Very Good Condition. Will Go Fast At $40.00. What A Great Christmas Gift!. Credit And Debit Cards Accepted. G&L");
            Assert.AreEqual(items[0].Date, DateTime.Parse("2015-07-01T17:36:10-07:00"));
        }
    }
}
