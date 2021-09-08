using LegendaryTools2022.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        : base("MyDBContext")
        {

        }
        public DbSet<Cards> EntityCards { get; set; }
        public DbSet<CardTypes> EntityCardTypes { get; set; }
        public DbSet<CustomSets> EntityCustomSets { get; set; }
        public DbSet<Decks> EntityDecks { get; set; }
        public DbSet<DeckTypes> EntityDeckTypes { get; set; }
        public DbSet<Templates> EntityTemplates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
