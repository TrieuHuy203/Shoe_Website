using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class Config
{
    public int ConfigId { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }
}
