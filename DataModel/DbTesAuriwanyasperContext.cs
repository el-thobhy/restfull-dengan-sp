using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiPointOfSales.DataModel;

public partial class DbTesAuriwanyasperContext : DbContext
{
    public DbTesAuriwanyasperContext()
    {
    }

    public DbTesAuriwanyasperContext(DbContextOptions<DbTesAuriwanyasperContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<SalesOrder> SalesOrders { get; set; }

    public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TableCustomerSeed());
        modelBuilder.ApplyConfiguration(new TablePriceSeed());
        modelBuilder.ApplyConfiguration(new TableProductSeed());
        modelBuilder.ApplyConfiguration(new TableSalesOrderSeed());
        modelBuilder.ApplyConfiguration(new TableSalesOrderDetailSeed());

    }

}
