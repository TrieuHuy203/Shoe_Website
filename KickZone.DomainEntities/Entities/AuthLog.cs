using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class AuthLog
{
    public int AuthLogId { get; set; }

    public int? UserId { get; set; }

    public string Action { get; set; } = null!;

    public string? Ipaddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual User? User { get; set; }
}
