using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPointOfSales.DataModel;

[Keyless]
[Table("Price")]
public partial class Price
{
    [StringLength(10)]
    [Unicode(false)]
    public string? ProductCode { get; set; }

    [Column("Price", TypeName = "money")]
    public decimal? Price1 { get; set; }

    [Column(TypeName = "date")]
    public DateTime? PriceValidateFrom { get; set; }

    [Column(TypeName = "date")]
    public DateTime? PriceValidateTo { get; set; }
}

public class TablePriceSeed : IEntityTypeConfiguration<Price>
{
    public void Configure(EntityTypeBuilder<Price> builder)
    {
        builder.HasNoKey();
    }
}
