using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPointOfSales.DataModel;

[Table("Product")]
public partial class Product
{
    [Key]
    [StringLength(10)]
    [Unicode(false)]
    public string ProductCode { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? ProductName { get; set; }
}

public class TableProductSeed : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new Product
            {
                ProductCode = "P001",
                ProductName = "Filosofi Teras",
            },
            new Product
            {
                ProductCode = "P002",
                ProductName = "Sepotong Hati Yang Baru",
            },
            new Product
            {
                ProductCode = "P003",
                ProductName = "How To Run Family Business",
            }
        );
    }
}
