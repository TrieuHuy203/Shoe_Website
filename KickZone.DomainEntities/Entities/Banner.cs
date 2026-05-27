using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class Banner
{
    public int BannerId { get; set; }

    public string? Title { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string? Link { get; set; }

    public bool? IsActive { get; set; }

    public int? DisplayOrder { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool IsDeleted { get; set; }
}
