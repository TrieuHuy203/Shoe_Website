using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class ReviewImage
{
    public int ReviewImageId { get; set; }

    public int ReviewId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public virtual Review Review { get; set; } = null!;
}
