using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class Refund
{
    public int RefundId { get; set; }

    public int OrderId { get; set; }

    public decimal Amount { get; set; }

    public string? Reason { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Order Order { get; set; } = null!;
}
