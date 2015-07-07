using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using LeapList.Models;
using LeapList.Parse;

namespace LeapList.Search
{
    public static class SearchItems
    {

        /// <summary>
        /// Searches for and returns a list of CLItem's that 
        /// meet the search criteria.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns>An enumerable of CLItems that met the search</returns>
        public static IEnumerable<CLItem> SearchFor(this XmlDocument doc, string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                return doc.GetItemList().Where(s => s.Title.Contains(searchText));
            }

            return Enumerable.Empty<CLItem>();
        }
    }
}