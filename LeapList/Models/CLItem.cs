using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Xml;

namespace LeapList.Models
{
    public class CLItem
    {
        [Key]
        public int ItemId { get; set; }
        public int SearchId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("SearchId")]
        public virtual SearchCriteria SearchCriteria { get; set; }
    }

    public static class CLParse
    {
        private static Regex RegPrice = new Regex(@".*&#x0024;(?<price>\d+)");

        public static List<CLItem> GetItemList(this XmlDocument doc)
        {
            XmlNodeList nodes = doc.GetElementsByTagName("item");
            List<CLItem> items = new List<CLItem>();
            foreach (XmlNode node in nodes)
            {
                CLItem item = new CLItem();
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name == "title")
                    {
                        item.Title = childNode.InnerText;
                        Match mat = RegPrice.Match(item.Title);
                        // Replacing the html symbols with human-readable symbols.
                        item.Title = item.Title.Replace("&amp;", "&").Replace("&#x0024;", "$");

                        if (mat.Success)
                        {
                            // Get price from the title.
                            item.Price = Convert.ToDecimal(mat.Groups["price"].Value);
                        }
                    }
                    if (childNode.Name == "description")
                    {
                        item.Description = childNode.InnerText;
                    }
                    if (childNode.Name == "link")
                    {
                        item.Link = childNode.InnerText;
                    }
                    if (childNode.Name == "dc:date")
                    {
                        item.Date = Convert.ToDateTime(childNode.InnerText);
                    }
                }
                if (!string.IsNullOrEmpty(item.Title))
                {
                    items.Add(item);
                }
            }
            return items;
        }
    }
}