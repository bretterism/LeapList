using LeapList.DataAccess;
using LeapList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CLWatcherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Access the DB.
            CLContext db = new CLContext();

            // 2. Look at most recent result for each category/search by profile.


            // 3. Get rss feed for step 2.
            // check out https://github.com/bretterism/LeapList/commit/4022f864a2b2250db38522968bc16c9dcc03e6bc
            // for test on how to use it.
            List<CLItem> items = new List<CLItem>();
            XmlDocument doc = new XmlDocument();

            items = doc.GetItemList();

            // 4. Check rss feed with latest results from step 2.

            // 5. Prepare email with list of new results.

            // 6. Send email.

            // 7. Set new latest result for each category.
        }
    }
}
