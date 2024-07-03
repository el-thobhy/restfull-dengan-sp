using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPointOfSales.DataModel;

[Table("SalesOrder")]
public partial class SalesOrder
{
    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [Key]
    [StringLength(10)]
    [Unicode(false)]
    public string SalesOrderNo { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string CustCode { get; set; } = null!;
}

public class TableSalesOrderSeed : IEntityTypeConfiguration<SalesOrder>
{
    public void Configure(EntityTypeBuilder<SalesOrder> builder)
    {
        builder.HasData(
            new SalesOrder
            {
                SalesOrderNo = "SO001",
                OrderDate = DateTime.Now,
                CustCode = "C001"
            },
            new SalesOrder
            {
                SalesOrderNo = "SO002",
                OrderDate = DateTime.Now,
                CustCode = "C002"
            },
            new SalesOrder
            {
                SalesOrderNo = "SO003",
                OrderDate = DateTime.Now,
                CustCode = "C003"
            }
        );
    }
}