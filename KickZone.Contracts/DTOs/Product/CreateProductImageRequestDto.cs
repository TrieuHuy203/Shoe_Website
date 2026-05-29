using Microsoft.AspNetCore.Http;
namespace KickZone.Contracts.DTOs.ProductImage;
public class CreateProductImageRequestDto
{
    public int ProductId { get; set; }
    public IFormFile File { get; set; } = null!;
    public bool IsMain { get; set; } = false;
}