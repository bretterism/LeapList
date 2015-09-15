using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LeapList.Models;
using System.Collections.Generic;

namespace LeapList.DataAccess
{
    public class CLContext : DbContext
    {
        public CLContext()
            : base("CLContext")
        {
            Database.SetInitializer<CLContext>(new CreateDatabaseIfNotExists<CLContext>());
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<SearchCriteria> SearchCriteria { get; set; }
        public DbSet<CLItem> CLItems { get; set; }
        public DbSet<CategorySearch> CategorySearches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public static class Transactions
    {
        public static void AddEntry(this CLContext db, UserProfile profile)
        {
            db.UserProfiles.Add(profile);
            db.SaveChanges();
        }
        public static void AddEntry(this CLContext db, SearchCriteria sc)
        {
            db.SearchCriteria.Add(sc);
            db.SaveChanges();

        }

        public static void DeleteEntry(this CLContext db, SearchCriteria sc)
        {
            db.SearchCriteria.Remove(sc);
            db.SaveChanges();
        }

        public static void AddEntry(this CLContext db, CategorySearch scc)
        {
            db.CategorySearches.Add(scc);
            db.SaveChanges();
        }

        public static void AddEntries(this CLContext db, List<CategorySearch> scc)
        {
            foreach (CategorySearch c in scc)
            {
                db.CategorySearches.Add(c);
            }
            db.SaveChanges();
        }

        public static void UpdateCityForProfile(this CLContext db, UserProfileSessionData profileData)
        {
            UserProfile profile = db.UserProfiles.Find(profileData.ProfileId);
            profile.City = profileData.City;

            db.UserProfiles.Attach(profile);
            var entry = db.Entry(profile);

            entry.Property(e => e.City).IsModified = true;
            db.SaveChanges();
        }
    }
}