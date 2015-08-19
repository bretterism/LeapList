﻿using System.Data.Entity;
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
            Database.SetInitializer<CLContext>(new DropCreateDatabaseIfModelChanges<CLContext>());
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<SearchCriteria> SearchCriteria { get; set; }
        public DbSet<CLItem> CLItems { get; set; }
        public DbSet<SC_Category> SC_Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public static class Transactions
    {
        public static void AddEntry(this CLContext db, Profile profile)
        {
            db.Profiles.Add(profile);
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

        public static void AddEntry(this CLContext db, SC_Category scc)
        {
            db.SC_Categories.Add(scc);
            db.SaveChanges();
        }

        public static void AddEntries(this CLContext db, List<SC_Category> scc)
        {
            foreach (SC_Category c in scc)
            {
                db.SC_Categories.Add(c);
            }
            db.SaveChanges();
        }
    }
}