using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int ProductVariantId { get; set; }

    public int Quantity { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
