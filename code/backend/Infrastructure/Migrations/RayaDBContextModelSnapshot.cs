﻿// <auto-generated />
using System;
using Infrastructure.Context.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(RayaDBContext))]
    partial class RayaDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2023, 12, 15, 14, 50, 59, 678, DateTimeKind.Local).AddTicks(110),
                            CreatedBy = "efcore seed",
                            Deleted = false,
                            Description = "Category 01",
                            ModifiedBy = "",
                            Name = "C01"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2023, 12, 15, 14, 50, 59, 678, DateTimeKind.Local).AddTicks(154),
                            CreatedBy = "efcore seed",
                            Deleted = false,
                            Description = "Category 02",
                            ModifiedBy = "",
                            Name = "C02"
                        },
                        new
                        {
                            Id = 3,
                            Created = new DateTime(2023, 12, 15, 14, 50, 59, 678, DateTimeKind.Local).AddTicks(158),
                            CreatedBy = "efcore seed",
                            Deleted = false,
                            Description = "Category 03",
                            ModifiedBy = "",
                            Name = "C03"
                        });
                });

            modelBuilder.Entity("Domain.Models.InventoryTransaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("InventoryTransactions");
                });

            modelBuilder.Entity("Domain.Models.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CategoryId = 1,
                            Code = "P01",
                            Created = new DateTime(2023, 12, 15, 14, 50, 59, 678, DateTimeKind.Local).AddTicks(279),
                            CreatedBy = "efcore seed",
                            Deleted = false,
                            Description = "",
                            ModifiedBy = "",
                            Name = "Product 01",
                            Quantity = 0,
                            UnitPrice = 0m
                        },
                        new
                        {
                            Id = 2L,
                            CategoryId = 1,
                            Code = "P02",
                            Created = new DateTime(2023, 12, 15, 14, 50, 59, 678, DateTimeKind.Local).AddTicks(286),
                            CreatedBy = "efcore seed",
                            Deleted = false,
                            Description = "",
                            ModifiedBy = "",
                            Name = "Product 02",
                            Quantity = 0,
                            UnitPrice = 0m
                        },
                        new
                        {
                            Id = 3L,
                            CategoryId = 2,
                            Code = "P03",
                            Created = new DateTime(2023, 12, 15, 14, 50, 59, 678, DateTimeKind.Local).AddTicks(288),
                            CreatedBy = "efcore seed",
                            Deleted = false,
                            Description = "",
                            ModifiedBy = "",
                            Name = "Product 03",
                            Quantity = 0,
                            UnitPrice = 0m
                        },
                        new
                        {
                            Id = 4L,
                            CategoryId = 3,
                            Code = "P04",
                            Created = new DateTime(2023, 12, 15, 14, 50, 59, 678, DateTimeKind.Local).AddTicks(291),
                            CreatedBy = "efcore seed",
                            Deleted = false,
                            Description = "",
                            ModifiedBy = "",
                            Name = "Product 04",
                            Quantity = 0,
                            UnitPrice = 0m
                        });
                });

            modelBuilder.Entity("Domain.Models.InventoryTransaction", b =>
                {
                    b.HasOne("Domain.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Models.Product", b =>
                {
                    b.HasOne("Domain.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domain.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}