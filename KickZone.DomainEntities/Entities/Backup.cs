using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class Backup
{
    public int BackupId { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public virtual User? CreatedByNavigation { get; set; }
}
