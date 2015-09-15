using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapList.Models;
using System.Web;

namespace CLWatcherApp
{
    //class RssPages
    //{
    //    public static string BuildHttp(AddEditSearchVM svm, UserProfile profile)
    //    {
    //        // http://stackoverflow.com/questions/20164298/net-how-to-build-a-url

    //        // Building search query.
    //        var query = HttpUtility.ParseQueryString(string.Empty);
    //        if (svm.MinPrice.HasValue)
    //            query["min_price"] = svm.MinPrice.ToString();
    //        if (svm.MaxPrice.HasValue)
    //            query["max_price"] = svm.MaxPrice.ToString();
    //        if (svm.SearchText != null)
    //            query["query"] = svm.SearchText;

    //        // TODO: Add more query options. Make them all nullable.
    //        query["format"] = "rss";

    //        // Building the full url here.
    //        UriBuilder url = new UriBuilder();
    //        url.Scheme = "https:";
    //        url.Host = profile.City + ".craigslist.org";

    //        // When there is no category specified, we search all.
    //        // Category all = "sss"
    //        if (svm.Categories.Count() > 0)
    //        {
    //            foreach (CheckBoxCategoryVM checkBox in svm.Categories)
    //            {
    //                url.Path = "search/" + checkBox.Code;
                    
    //            }
    //        }
    //        else
    //        {
    //            url.Path = "search/sss";
    //        }
    //        url.Query = query.ToString();
    //        return url.ToString();
    //    }
    //}
}
