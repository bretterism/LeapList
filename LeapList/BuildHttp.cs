﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapList.Models;
using System.Web;

namespace LeapList
{
    class RssPages
    {
        public static string BuildHttp(AddEditSearchVM svm, string category, string city)
        {
            // http://stackoverflow.com/questions/20164298/net-how-to-build-a-url

            // Building search query.
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (svm.MinPrice.HasValue)
                query["min_price"] = svm.MinPrice.ToString();
            if (svm.MaxPrice.HasValue)
                query["max_price"] = svm.MaxPrice.ToString();
            if (svm.SearchText != null)
                query["query"] = svm.SearchText;

            // TODO: Add more query options. Make them all nullable.
            query["format"] = "rss";

            // Building the full url here.
            UriBuilder url = new UriBuilder();
            url.Scheme = "https:";
            url.Host = city + ".craigslist.org";

            // When there is no category specified, we search all.
            // Category all = "sss"
            url.Path = (category != null ? "search/" + category : "search/sss");
            url.Query = query.ToString();

            return url.ToString();
        }
    }
}
