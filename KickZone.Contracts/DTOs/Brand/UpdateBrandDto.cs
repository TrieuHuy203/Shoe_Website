namespace KickZone.Contracts.DTOs.BrandDTOs;

public class UpdateBrandDto
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Logo { get; set; }

    public bool? IsActive { get; set; }
}