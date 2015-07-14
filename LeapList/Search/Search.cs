using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using LeapList.Models;

namespace LeapList.Search
{
    public static class SearchItems
    {
        public static string BuildHttp(SearchCriteria sc, Profile profile)
        {
            // http://stackoverflow.com/questions/20164298/net-how-to-build-a-url

            // Building search query.
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (sc.MinPrice.HasValue)
                query["min_price"] = sc.MinPrice.ToString();
            if (sc.MaxPrice.HasValue)
                query["max_price"] = sc.MaxPrice.ToString();
            if (sc.SearchText != null)
                query["query"] = sc.SearchText;

            // TODO: Add more query options. Make them all nullable.
            query["format"] = "rss";

            // Building the full url here.
            UriBuilder url = new UriBuilder();
            url.Scheme = "https:";
            url.Host = profile.City + ".craigslist.org";

            // When there is no category specified, we search all.
            // Category all = "sss"
            url.Path = (sc.Category != null ? "search/" + sc.Category : "search/sss");
            url.Query = query.ToString();
           
            return url.ToString();
        }
    }
}