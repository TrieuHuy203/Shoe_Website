using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class Complaint
{
    public int ComplaintId { get; set; }

    public int UserId { get; set; }

    public int? OrderId { get; set; }

    public string Content { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Order? Order { get; set; }

    public virtual User User { get; set; } = null!;
}
