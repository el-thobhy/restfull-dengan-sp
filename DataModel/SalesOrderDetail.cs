using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPointOfSales.DataModel;

[PrimaryKey("SalesOrderNo", "ProductCode")]
[Table("SalesOrderDetail")]
public partial class SalesOrderDetail
{
    [Key]
    [StringLength(10)]
    [Unicode(false)]
    public string SalesOrderNo { get; set; } = null!;

    [Key]
    [StringLength(10)]
    [Unicode(false)]
    public string ProductCode { get; set; } = null!;

    public int Qty { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }
}

public class TableSalesOrderDetailSeed : IEntityTypeConfiguration<SalesOrderDetail>
{
    public void Configure(EntityTypeBuilder<SalesOrderDetail> builder)
    {
        builder.HasData(
            new SalesOrderDetail
            {
                SalesOrderNo = "SO001",
                ProductCode = "P001",
                Qty = 1,
                Price = 97000
            },
            new SalesOrderDetail
            {
                SalesOrderNo = "SO002",
                ProductCode = "P002",
                Qty = 1,
                Price = 109000
            },
            new SalesOrderDetail
            {
                SalesOrderNo = "SO003",
                ProductCode = "P003",
                Qty = 1,
                Price = 120000
            }
        );
    }
}
