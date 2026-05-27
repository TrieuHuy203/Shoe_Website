using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class Address
{
    public int AddressId { get; set; }

    public int UserId { get; set; }

    public string RecipientName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string? Ward { get; set; }

    public string? District { get; set; }

    public string? Province { get; set; }

    public bool? IsDefault { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<ShippingInfo> ShippingInfos { get; set; } = new List<ShippingInfo>();

    public virtual User User { get; set; } = null!;
}
