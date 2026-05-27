namespace KickZone.Contracts.DTOs.BrandDTOs;

public class BrandResponseDto
{
    public int BrandId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Logo { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}