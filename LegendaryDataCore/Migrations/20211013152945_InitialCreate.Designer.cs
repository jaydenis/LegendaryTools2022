﻿// <auto-generated />
using System;
using LegendaryDataCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LegendaryDataCore.Migrations
{
    [DbContext(typeof(LegendaryContext))]
    [Migration("20211013152945_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("LegendaryDataCore.Models.HenchmenModel", b =>
                {
                    b.Property<int>("HenchmenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HenchmenName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SetId")
                        .HasColumnType("INTEGER");

                    b.HasKey("HenchmenId");

                    b.HasIndex("SetId");

                    b.ToTable("Henchmen");
                });

            modelBuilder.Entity("LegendaryDataCore.Models.HeroModel", b =>
                {
                    b.Property<int>("HeroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HeroName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SetId")
                        .HasColumnType("INTEGER");

                    b.HasKey("HeroId");

                    b.HasIndex("SetId");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("LegendaryDataCore.Models.LegendarySetModel", b =>
                {
                    b.Property<int>("SetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Owned")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Set")
                        .HasColumnType("TEXT");

                    b.HasKey("SetId");

                    b.ToTable("Sets");
                });

            modelBuilder.Entity("LegendaryDataCore.Models.MastermindModel", b =>
                {
                    b.Property<int>("MastermindId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Henchman")
                        .HasColumnType("TEXT");

                    b.Property<string>("MastermindName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SetId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Villain")
                        .HasColumnType("TEXT");

                    b.HasKey("MastermindId");

                    b.HasIndex("SetId");

                    b.ToTable("Masterminds");
                });

            modelBuilder.Entity("LegendaryDataCore.Models.SchemeModel", b =>
                {
                    b.Property<int>("SchemeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HenchmenName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SetId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SchemeId");

                    b.HasIndex("SetId");

                    b.ToTable("Schemes");
                });

            modelBuilder.Entity("LegendaryDataCore.Models.VillainModel", b =>
                {
                    b.Property<int>("VillainId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SetId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VillainName")
                        .HasColumnType("TEXT");

                    b.HasKey("VillainId");

                    b.HasIndex("SetId");

                    b.ToTable("Villains");
                });

            modelBuilder.Entity("LegendaryDataCore.Models.HenchmenModel", b =>
                {
                    b.HasOne("LegendaryDataCore.Models.LegendarySetModel", "Set")
                        .WithMany("Henchmen")
                        .HasForeignKey("SetId");

                    b.Navigation("Set");
                });

            modelBuilder.Entity("LegendaryDataCore.Models.HeroModel", b =>
                {
                    b.HasOne("LegendaryDataCore.Models.LegendarySetModel", "Set")
                        .WithMany("Heroes")
                        .HasForeignKey("SetId");

                    b.Navigation("Set");
                });

            modelBuilder.Entity("LegendaryDataCore.Models.MastermindModel", b =>
                {
                    b.HasOne("LegendaryDataCore.Models.LegendarySetModel", "Set")
                        .WithMany("Masterminds")
                        .HasForeignKey("SetId");

                    b.Navigation("Set");
                });

            modelBuilder.Entity("LegendaryDataCore.Models.SchemeModel", b =>
                {
                    b.HasOne("LegendaryDataCore.Models.LegendarySetModel", "Set")
                        .WithMany("Schemes")
                        .HasForeignKey("SetId");

                    b.Navigation("Set");
                });

            modelBuilder.Entity("LegendaryDataCore.Models.VillainModel", b =>
                {
                    b.HasOne("LegendaryDataCore.Models.LegendarySetModel", "Set")
                        .WithMany("Villains")
                        .HasForeignKey("SetId");

                    b.Navigation("Set");
                });

            modelBuilder.Entity("LegendaryDataCore.Models.LegendarySetModel", b =>
                {
                    b.Navigation("Henchmen");

                    b.Navigation("Heroes");

                    b.Navigation("Masterminds");

                    b.Navigation("Schemes");

                    b.Navigation("Villains");
                });
#pragma warning restore 612, 618
        }
    }
}
