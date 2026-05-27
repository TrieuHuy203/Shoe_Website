using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class ShippingFee
{
    public int ShippingFeeId { get; set; }

    public string Location { get; set; } = null!;

    public decimal Fee { get; set; }

    public string? Note { get; set; }
}
