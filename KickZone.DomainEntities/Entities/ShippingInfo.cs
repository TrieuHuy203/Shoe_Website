using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class ShippingInfo
{
    public int ShippingInfoId { get; set; }

    public int UserId { get; set; }

    public string RecipientName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Ward { get; set; }

    public string? District { get; set; }

    public string? Province { get; set; }

    public bool? IsDefault { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? AddressId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Address? AddressNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
