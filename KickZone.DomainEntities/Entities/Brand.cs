using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class Brand
{
    public int BrandId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Logo { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsActive { get; set; } = true;

public DateTime CreatedAt { get; set; }

public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
