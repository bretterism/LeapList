namespace LeapList.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using LeapList.Models;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<LeapList.DataAccess.CLContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LeapList.DataAccess.CLContext context)
        {
            if (!context.UserProfiles.Any())
            {
                var profile = new List<UserProfile>
            {
                new UserProfile {ProfileId = 1, City = "Corvallis", Username = "brett", PasswordHash = GetHash("abc123")},
                new UserProfile {ProfileId = 2, City = "Eugene", Username = "mom", PasswordHash = GetHash("abc123")},
                new UserProfile {ProfileId = 3, City = "Portland", Username = "devin", PasswordHash = GetHash("abc123")}
            };

                profile.ForEach(s => context.UserProfiles.AddOrUpdate(s));
            }

            if (!context.SearchCriteria.Any())
            {
                var searchCriteria = new List<SearchCriteria>
            {
                new SearchCriteria {SearchId = 1, ProfileId = 1, MaxPrice = 50, SearchText = "antique"},
                new SearchCriteria {SearchId = 2, ProfileId = 1, MinPrice = 500, MaxPrice = 1000, SearchText = "speakers"},
                new SearchCriteria {SearchId = 3, ProfileId = 2, MinPrice = 100, MaxPrice = 200, SearchText = "chair"},
                new SearchCriteria {SearchId = 4, ProfileId = 3, SearchText = "mario"},
                new SearchCriteria {SearchId = 5, ProfileId = 3, MaxPrice = 300, SearchText = "tent"},
                new SearchCriteria {SearchId = 6, ProfileId = 3, MinPrice = 75, SearchText = "bookshelf"}
            };

                searchCriteria.ForEach(s => context.SearchCriteria.AddOrUpdate(s));
            }

            if (!context.CLItems.Any())
            {
                var cLItem = new List<CLItem>
            {
                new CLItem {ItemId = 1, CategoryId = 1, Title = "antique chair", Price = 30, Date = DateTime.Parse("7/10/2015 08:15:12 AM")},
                new CLItem {ItemId = 2, CategoryId = 1, Title = "vintage antique vanity", Price = 50, Date = DateTime.Parse("7/10/2015 09:15:12 AM")},
                new CLItem {ItemId = 3, CategoryId = 2, Title = "Giant subwoofer speakers", Price = 700, Date = DateTime.Parse("7/10/2015 10:15:12 AM")},
                new CLItem {ItemId = 4, CategoryId = 3, Title = "desk and chair", Price = 150, Date = DateTime.Parse("7/10/2015 11:15:12 AM")},
                new CLItem {ItemId = 5, CategoryId = 4, Title = "super mario bros", Price = 20, Date = DateTime.Parse("7/10/2015 12:15:12 PM")},
                new CLItem {ItemId = 6, CategoryId = 5, Title = "3 person tent", Price = 250, Date = DateTime.Parse("7/10/2015 01:15:12 PM")},
                new CLItem {ItemId = 7, CategoryId = 6, Title = "5 tier bookshelf", Price = 75, Date = DateTime.Parse("7/10/2015 02:15:12 PM")},
                new CLItem {ItemId = 8, CategoryId = 6, Title = "dark walnut bookshelf", Price = 150, Date = DateTime.Parse("7/10/2015 03:15:12 PM")},
            };

                cLItem.ForEach(s => context.CLItems.AddOrUpdate(s));
            }

            if (!context.CategorySearches.Any())
            {
                var categorySearches = new List<CategorySearch>
            {
                // TODO: get valid SearchLinks.
                new CategorySearch {CategoryId = 1, SearchId = 1, Category = "ata", SearchLink = "N/A"},
                new CategorySearch {CategoryId = 2, SearchId = 1, Category = "bfa", SearchLink = "N/A"},
                new CategorySearch {CategoryId = 3, SearchId = 2, Category = "ela", SearchLink = "N/A"},
                new CategorySearch {CategoryId = 4, SearchId = 2, Category = "msa", SearchLink = "N/A"},
                new CategorySearch {CategoryId = 5, SearchId = 3, Category = "fua", SearchLink = "N/A"},
                new CategorySearch {CategoryId = 6, SearchId = 3, Category = "ata", SearchLink = "N/A"},
                new CategorySearch {CategoryId = 7, SearchId = 4, Category = "vga", SearchLink = "N/A"},
                new CategorySearch {CategoryId = 8, SearchId = 4, Category = "ema", SearchLink = "N/A"},
                new CategorySearch {CategoryId = 9, SearchId = 5, Category = "sga", SearchLink = "N/A"},
                new CategorySearch {CategoryId = 10, SearchId = 5, Category = "rva", SearchLink = "N/A"},
                new CategorySearch {CategoryId = 11, SearchId = 6, Category = "fua", SearchLink = "N/A"},
                new CategorySearch {CategoryId = 12, SearchId = 6, Category = "gms", SearchLink = "N/A"}
            };

                categorySearches.ForEach(s => context.CategorySearches.AddOrUpdate(s));
            }
        }

        private string GetHash(string plainTextPassword)
        { 
            return BitConverter.ToString(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(plainTextPassword))).Replace("-", string.Empty);
        }
    }
}
