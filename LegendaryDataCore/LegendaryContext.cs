using LegendaryDataCore.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LegendaryDataCore
{
    public class LegendaryContext : DbContext
    {
        public DbSet<LegendarySetModel> Sets { get; set; }
        public DbSet<MastermindModel> Masterminds { get; set; }
        public DbSet<HeroModel> Heroes { get; set; }
        public DbSet<VillainModel> Villains { get; set; }
        public DbSet<HenchmenModel> Henchmen { get; set; }
        public DbSet<SchemeModel> Schemes { get; set; }

        public string DbPath { get; private set; }

        public LegendaryContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}legendary_data.db";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

             modelBuilder.Entity<MastermindModel>()
                .HasOne(p => p.Set)
                .WithMany(b => b.Masterminds);
                    

            modelBuilder.Entity<HeroModel>()
                .HasOne(p => p.Set)
                .WithMany(b => b.Heroes);

            modelBuilder.Entity<VillainModel>()
                .HasOne(p => p.Set)
                .WithMany(b => b.Villains);

            modelBuilder.Entity<HenchmenModel>()
                .HasOne(p => p.Set)
                .WithMany(b => b.Henchmen);

            modelBuilder.Entity<SchemeModel>()
                .HasOne(p => p.Set)
                .WithMany(b => b.Schemes);


        }


        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
