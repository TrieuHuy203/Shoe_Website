using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class ActivityLog
{
    public int ActivityLogId { get; set; }

    public int? UserId { get; set; }

    public string Action { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Ipaddress { get; set; }

    public string? UserAgent { get; set; }

    public virtual User? User { get; set; }
}
