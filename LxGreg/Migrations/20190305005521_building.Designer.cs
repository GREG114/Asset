﻿// <auto-generated />
using System;
using LxGreg.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LxGreg.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190305005521_building")]
    partial class building
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LxGreg.Models.Item", b =>
                {
                    b.Property<string>("ItemNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ItemName");

                    b.Property<string>("ItemShortNumber");

                    b.Property<string>("Mark");

                    b.Property<string>("Model");

                    b.Property<int>("storeId");

                    b.HasKey("ItemNumber");

                    b.HasIndex("storeId");

                    b.ToTable("items");
                });

            modelBuilder.Entity("LxGreg.Models.Manager", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("managers");
                });

            modelBuilder.Entity("LxGreg.Models.Order", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Mark");

                    b.Property<string>("OperaterId");

                    b.Property<DateTime>("OrderTime");

                    b.Property<int>("Quantity");

                    b.Property<string>("TakerId");

                    b.Property<string>("itemItemNumber");

                    b.Property<bool>("take");

                    b.Property<int>("unitId");

                    b.HasKey("Id");

                    b.HasIndex("OperaterId");

                    b.HasIndex("TakerId");

                    b.HasIndex("itemItemNumber");

                    b.HasIndex("unitId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("LxGreg.Models.Stock", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrentQuantity");

                    b.Property<string>("itemItemNumber");

                    b.Property<int>("unitId");

                    b.HasKey("id");

                    b.HasIndex("itemItemNumber");

                    b.HasIndex("unitId");

                    b.ToTable("stocks");
                });

            modelBuilder.Entity("LxGreg.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StoreName");

                    b.HasKey("Id");

                    b.ToTable("stores");
                });

            modelBuilder.Entity("LxGreg.Models.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UnitName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("UnitName");

                    b.ToTable("units");
                });

            modelBuilder.Entity("LxGreg.Models.Item", b =>
                {
                    b.HasOne("LxGreg.Models.Store", "store")
                        .WithMany("items")
                        .HasForeignKey("storeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LxGreg.Models.Order", b =>
                {
                    b.HasOne("LxGreg.Models.Manager", "Operater")
                        .WithMany()
                        .HasForeignKey("OperaterId");

                    b.HasOne("LxGreg.Models.Manager", "Taker")
                        .WithMany()
                        .HasForeignKey("TakerId");

                    b.HasOne("LxGreg.Models.Item", "item")
                        .WithMany("orders")
                        .HasForeignKey("itemItemNumber");

                    b.HasOne("LxGreg.Models.Unit", "unit")
                        .WithMany("orders")
                        .HasForeignKey("unitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LxGreg.Models.Stock", b =>
                {
                    b.HasOne("LxGreg.Models.Item", "item")
                        .WithMany("stocks")
                        .HasForeignKey("itemItemNumber");

                    b.HasOne("LxGreg.Models.Unit", "unit")
                        .WithMany("stocks")
                        .HasForeignKey("unitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}