using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? PaymentId { get; set; }

    public string Status { get; set; } = null!;

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Payment? Payment { get; set; }
}
