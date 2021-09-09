using LegendaryTools2022.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Data
{
    public class DataContext : DbContext
    {

        private static bool _created = false;
        public DataContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

           // optionsBuilder.UseSqlite(@"DataSource=C:\Repos\LegendaryTools2022\LegendaryTools2022\Data\legendary_tools_db.sqlite;");
        }
        public DbSet<Cards> EntityCards { get; set; }
        public DbSet<CardTypes> EntityCardTypes { get; set; }
        public DbSet<CustomSets> EntityCustomSets { get; set; }
        public DbSet<Decks> EntityDecks { get; set; }
        public DbSet<DeckTypes> EntityDeckTypes { get; set; }
        public DbSet<Templates> EntityTemplates { get; set; }

        
    }
}
