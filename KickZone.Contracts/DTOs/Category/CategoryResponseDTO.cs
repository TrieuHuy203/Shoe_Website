namespace KickZone.Contracts.DTOs.Category;
public class CategoryResponseDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int? ParentId { get; set; }
    public bool IsDeleted { get; set; }
    public string? ParentName { get; set; }
}