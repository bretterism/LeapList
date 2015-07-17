using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LeapList.Models;

namespace LeapList.DataAccess
{
    public class CLContext : DbContext
    {
        public CLContext() : base("CLContext")
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<SearchCriteria> SearchCriteria { get; set; }
        public DbSet<CLItem> CLItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public static class Transactions
    {
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

        
    }
}