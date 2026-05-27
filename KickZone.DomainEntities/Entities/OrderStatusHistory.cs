using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class OrderStatusHistory
{
    public int StatusHistoryId { get; set; }

    public int OrderId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? ChangedAt { get; set; }

    public int? ChangedBy { get; set; }

    public string? Note { get; set; }

    public virtual User? ChangedByNavigation { get; set; }

    public virtual Order Order { get; set; } = null!;
}
