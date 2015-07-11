using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LeapList.Models;

namespace LeapList.DataAccess
{
    public class CLInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CLContext>
    {
        protected override void Seed(CLContext context)
        {
            var profile = new List<Profile>
            {
                new Profile {ProfileId = 1, City = "Corvallis", Username = "brett"},
                new Profile {ProfileId = 2, City = "Eugene", Username = "mom"},
                new Profile {ProfileId = 3, City = "Portland", Username = "devin"}
            };

            profile.ForEach(s => context.Profiles.Add(s));

            var searchCriteria = new List<SearchCriteria>
            {
                new SearchCriteria {SearchId = 1, ProfileId = 1, Category = "ata", MaxPrice = 50, SearchText = "antique"},
                new SearchCriteria {SearchId = 2, ProfileId = 1, Category = "sss", MinPrice = 500, MaxPrice = 1000, SearchText = "speakers"},
                new SearchCriteria {SearchId = 3, ProfileId = 2, Category = "fua", MinPrice = 100, MaxPrice = 200, SearchText = "chair"},
                new SearchCriteria {SearchId = 4, ProfileId = 3, Category = "vga", SearchText = "mario"},
                new SearchCriteria {SearchId = 5, ProfileId = 3, Category = "sga", MaxPrice = 300, SearchText = "tent"},
                new SearchCriteria {SearchId = 6, ProfileId = 3, Category = "ata", MinPrice = 75, SearchText = "bookshelf"}
            };

            searchCriteria.ForEach(s => context.SearchCriteria.Add(s));

            var cLItem = new List<CLItem>
            {
                new CLItem {ItemId = 1, SearchId = 1, Title = "antique chair", Price = 30, Date = DateTime.Parse("7/10/2015 08:15:12 AM")},
                new CLItem {ItemId = 2, SearchId = 1, Title = "vintage antique vanity", Price = 50, Date = DateTime.Parse("7/10/2015 09:15:12 AM")},
                new CLItem {ItemId = 3, SearchId = 2, Title = "Giant subwoofer speakers", Price = 700, Date = DateTime.Parse("7/10/2015 10:15:12 AM")},
                new CLItem {ItemId = 4, SearchId = 3, Title = "desk and chair", Price = 150, Date = DateTime.Parse("7/10/2015 11:15:12 AM")},
                new CLItem {ItemId = 5, SearchId = 4, Title = "super mario bros", Price = 20, Date = DateTime.Parse("7/10/2015 12:15:12 PM")},
                new CLItem {ItemId = 6, SearchId = 5, Title = "3 person tent", Price = 250, Date = DateTime.Parse("7/10/2015 01:15:12 PM")},
                new CLItem {ItemId = 7, SearchId = 6, Title = "5 tier bookshelf", Price = 75, Date = DateTime.Parse("7/10/2015 02:15:12 PM")},
                new CLItem {ItemId = 8, SearchId = 6, Title = "dark walnut bookshelf", Price = 150, Date = DateTime.Parse("7/10/2015 03:15:12 PM")},
            };

            cLItem.ForEach(s => context.CLItems.Add(s));
        }
    }
}