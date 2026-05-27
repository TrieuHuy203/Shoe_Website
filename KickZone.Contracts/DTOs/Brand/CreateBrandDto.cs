namespace KickZone.Contracts.DTOs.BrandDTOs;

public class CreateBrandDto
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Logo { get; set; }
}