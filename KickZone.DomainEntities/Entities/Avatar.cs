using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class Avatar
{
    public int AvatarId { get; set; }

    public int UserId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool? IsCurrent { get; set; }

    public DateTime? UploadedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
