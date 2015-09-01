using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeapList.Models
{
    public static class DictCategory
    {
        private static Dictionary<string, string> DCategory;

        static DictCategory()
        {
            if (DCategory == null)
            {
                LookupCategory();
            }
        }

        private static void LookupCategory()
        {
            DCategory = new Dictionary<string, string>()
            {
                {"ata", "antiques"},
                {"ppa", "appliances"},
                {"ara", "arts & crafts"},
                {"sna", "atv, utv, snow"},
                {"pta", "auto parts"},
                {"baa", "baby & kid"},
                {"bar", "barter"},
                {"haa", "beauty & health"},
                {"bia", "bikes"}, // all bicycles
                {"boo", "boats"}, // all boats
                {"bka", "books"},
                {"bfa", "business"},
                {"cta", "cars & trucks"}, // all cars & trucks
                {"ema", "cds, dvds, vhs"},
                {"moa", "cell phones"},
                {"cla", "clothes & acc"},
                {"cba", "collectables"},
                {"sya", "computers"}, // all computers
                {"ela", "electronics"},
                {"gra", "farm & garden"},
                {"zip", "free"},
                {"fua", "furniture"},
                {"gms", "garage sale"},
                {"foa", "general"},
                {"hva", "heavy equipment"},
                {"hsa", "household"},
                {"jwa", "jewelry"},
                {"maa", "materials"},
                {"mca", "motorcycles"}, // all motorcycles
                {"msa", "music instruments"},
                {"pha", "photo & video equipment"},
                {"rva", "rvs & camp"},
                {"sga", "sporting goods"},
                {"tia", "tickets"},
                {"tla", "tools"},
                {"taa", "toys & games"},
                {"vga", "video gaming"},
                {"waa", "wanted"},
                // {"sss", "all"} if no checkbox is clicked, we search all.
            };
        }

        public static Dictionary<string, string> GetCategories()
        {
            return DCategory;
        }

        public static string GetCategoryName(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new FormatException("Category key cannot be empty.");
            }

            return DCategory[key];
        }

        public static List<string> GetCagetoryNames()
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, string> cat in DCategory)
            {
                list.Add(cat.Value);
            }
            return list;
        }

        public static List<string> GetCagetoryKeys()
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, string> cat in DCategory)
            {
                list.Add(cat.Key);
            }
            return list;
        }

        public static bool IsCategoryKey(string key)
        {
            return DCategory.ContainsKey(key);
        }

        public static bool IsCategoryValue(string value)
        {
            return DCategory.ContainsValue(value);
        }
    }
}