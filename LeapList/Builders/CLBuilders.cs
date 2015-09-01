using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeapList.Builders
{
    public static class CLBuilders
    {
        public static string CLUrlBuilder(string category)
        {
            UriBuilder url = new UriBuilder("http:", "craigslist.org");
            return url.ToString();
        }
    }
}