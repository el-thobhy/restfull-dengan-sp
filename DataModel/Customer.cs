using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPointOfSales.DataModel;

[Keyless]
[Table("Customer")]
public partial class Customer
{
    [StringLength(10)]
    [Unicode(false)]
    public string? CustId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CustName { get; set; }
}

public class TableCustomerSeed : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasNoKey();
    }
}
