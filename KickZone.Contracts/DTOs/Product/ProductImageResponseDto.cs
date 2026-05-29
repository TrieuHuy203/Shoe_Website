namespace KickZone.Contracts.DTOs.ProductImage;
public class ProductImageResponseDto
{
    public int ImageId { get; set; }
    public int ProductId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public bool IsMain { get; set; }
    public int SortOrder { get; set; }
}