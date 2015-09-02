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
            var profile = new List<Profile>
            {
                new Profile {ProfileId = 1, City = "Corvallis", Username = "brett", PasswordHash = GetHash("abc123")},
                new Profile {ProfileId = 2, City = "Eugene", Username = "mom", PasswordHash = GetHash("abc123")},
                new Profile {ProfileId = 3, City = "Portland", Username = "devin", PasswordHash = GetHash("abc123")}
            };

            profile.ForEach(s => context.Profiles.AddOrUpdate(s));

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

            var categorySearches = new List<CategorySearch>
            {
                new CategorySearch {CategoryId = 1, SearchId = 1, Category = "ata"},
                new CategorySearch {CategoryId = 2, SearchId = 1, Category = "bfa"},
                new CategorySearch {CategoryId = 3, SearchId = 2, Category = "ela"},
                new CategorySearch {CategoryId = 4, SearchId = 2, Category = "msa"},
                new CategorySearch {CategoryId = 5, SearchId = 3, Category = "fua"},
                new CategorySearch {CategoryId = 6, SearchId = 3, Category = "ata"},
                new CategorySearch {CategoryId = 7, SearchId = 4, Category = "vga"},
                new CategorySearch {CategoryId = 8, SearchId = 4, Category = "ema"},
                new CategorySearch {CategoryId = 9, SearchId = 5, Category = "sga"},
                new CategorySearch {CategoryId = 10, SearchId = 5, Category = "rva"},
                new CategorySearch {CategoryId = 11, SearchId = 6, Category = "fua"},
                new CategorySearch {CategoryId = 12, SearchId = 6, Category = "gms"}
            };

            categorySearches.ForEach(s => context.SC_Categories.AddOrUpdate(s));
        }

        private string GetHash(string plainTextPassword)
        { 
            return BitConverter.ToString(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(plainTextPassword))).Replace("-", string.Empty);
        }
    }
}
