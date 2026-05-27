namespace KickZone.Contracts.DTOs.Category;
public class CreateCategoryRequestDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int? ParentId { get; set; }
    public string? ImageUrl { get; set; }
}