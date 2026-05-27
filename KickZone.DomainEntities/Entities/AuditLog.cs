using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class AuditLog
{
    public int AuditLogId { get; set; }

    public int? UserId { get; set; }

    public string Action { get; set; } = null!;

    public string? TableName { get; set; }

    public int? RecordId { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual User? User { get; set; }
}
