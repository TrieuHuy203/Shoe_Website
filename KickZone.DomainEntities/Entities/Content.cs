using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class Content
{
    public int ContentId { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Body { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }
}
