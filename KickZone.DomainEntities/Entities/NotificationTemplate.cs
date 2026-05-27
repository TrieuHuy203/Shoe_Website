using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class NotificationTemplate
{
    public int TemplateId { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? Type { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }
}
