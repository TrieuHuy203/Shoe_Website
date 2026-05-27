using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public int CartId { get; set; }

    public int ProductVariantId { get; set; }

    public int Quantity { get; set; }

    public DateTime? AddedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
