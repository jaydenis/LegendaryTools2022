using LegendaryCardEditor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Data
{
    public class DataContext : DbContext
    {

        public DataContext()
        {
           
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite(@"DataSource=C:\Repos\LegendaryTools2022\LegendaryCardEditor\Data\legendary_tools_db.sqlite;");

            
        }
    

        public DbSet<Cards> EntityCards { get; set; }
        public DbSet<CardTypes> EntityCardTypes { get; set; }
        public DbSet<CustomSets> EntityCustomSets { get; set; }
        public DbSet<Decks> EntityDecks { get; set; }
        public DbSet<DeckTypes> EntityDeckTypes { get; set; }
        public DbSet<Templates> EntityTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomSets>().HasMany(c => c.Decks).WithOne(i => i.CustomSet);
            modelBuilder.Entity<Decks>().HasMany(c => c.Cards).WithOne(i => i.Deck);

        }


    }



    public class MyConfiguration : DbConfiguration, IDbConnectionFactory
    {
        public MyConfiguration()
        {
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);

            var providerServices = (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices));

            SetProviderServices("System.Data.SQLite", providerServices);
            SetProviderServices("System.Data.SQLite.EF6", providerServices);

            SetDefaultConnectionFactory(this);
        }

        public DbConnection CreateConnection(string connectionString)
            => new SQLiteConnection(connectionString);
    }

}
