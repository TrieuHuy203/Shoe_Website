using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class ProductVariant
{
    public int VariantId { get; set; }

    public int ProductId { get; set; }

    public string? Size { get; set; }

    public string? Color { get; set; }

    public string? Type { get; set; }

    public string? Sku { get; set; }

    public decimal? Price { get; set; }

    

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Inventory? Inventory { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product Product { get; set; } = null!;
}
