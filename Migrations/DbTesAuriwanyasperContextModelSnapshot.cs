﻿// <auto-generated />
using System;
using ApiPointOfSales.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiPointOfSales.Migrations
{
    [DbContext(typeof(DbTesAuriwanyasperContext))]
    partial class DbTesAuriwanyasperContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiPointOfSales.DataModel.Customer", b =>
                {
                    b.Property<string>("CustId")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CustName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("ApiPointOfSales.DataModel.Price", b =>
                {
                    b.Property<decimal?>("Price1")
                        .HasColumnType("money")
                        .HasColumnName("Price");

                    b.Property<DateTime?>("PriceValidateFrom")
                        .HasColumnType("date");

                    b.Property<DateTime?>("PriceValidateTo")
                        .HasColumnType("date");

                    b.Property<string>("ProductCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.ToTable("Price");
                });

            modelBuilder.Entity("ApiPointOfSales.DataModel.Product", b =>
                {
                    b.Property<string>("ProductCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("ProductName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("ProductCode");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            ProductCode = "P001",
                            ProductName = "Filosofi Teras"
                        },
                        new
                        {
                            ProductCode = "P002",
                            ProductName = "Sepotong Hati Yang Baru"
                        },
                        new
                        {
                            ProductCode = "P003",
                            ProductName = "How To Run Family Business"
                        });
                });

            modelBuilder.Entity("ApiPointOfSales.DataModel.SalesOrder", b =>
                {
                    b.Property<string>("SalesOrderNo")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CustCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime");

                    b.HasKey("SalesOrderNo");

                    b.ToTable("SalesOrder");

                    b.HasData(
                        new
                        {
                            SalesOrderNo = "SO001",
                            CustCode = "C001",
                            OrderDate = new DateTime(2024, 7, 2, 16, 51, 1, 20, DateTimeKind.Local).AddTicks(6751)
                        },
                        new
                        {
                            SalesOrderNo = "SO002",
                            CustCode = "C002",
                            OrderDate = new DateTime(2024, 7, 2, 16, 51, 1, 20, DateTimeKind.Local).AddTicks(6762)
                        },
                        new
                        {
                            SalesOrderNo = "SO003",
                            CustCode = "C003",
                            OrderDate = new DateTime(2024, 7, 2, 16, 51, 1, 20, DateTimeKind.Local).AddTicks(6763)
                        });
                });

            modelBuilder.Entity("ApiPointOfSales.DataModel.SalesOrderDetail", b =>
                {
                    b.Property<string>("SalesOrderNo")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("ProductCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.HasKey("SalesOrderNo", "ProductCode");

                    b.ToTable("SalesOrderDetail");

                    b.HasData(
                        new
                        {
                            SalesOrderNo = "SO001",
                            ProductCode = "P001",
                            Price = 97000m,
                            Qty = 1
                        },
                        new
                        {
                            SalesOrderNo = "SO002",
                            ProductCode = "P002",
                            Price = 109000m,
                            Qty = 1
                        },
                        new
                        {
                            SalesOrderNo = "SO003",
                            ProductCode = "P003",
                            Price = 120000m,
                            Qty = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
