using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class ProductPriceHistory
{
    public int PriceHistoryId { get; set; }

    public int ProductId { get; set; }

    public decimal OldPrice { get; set; }

    public decimal NewPrice { get; set; }

    public DateTime ChangedAt { get; set; }

    public int? ChangedBy { get; set; }

    public virtual User? ChangedByNavigation { get; set; }

    public virtual Product Product { get; set; } = null!;
}
