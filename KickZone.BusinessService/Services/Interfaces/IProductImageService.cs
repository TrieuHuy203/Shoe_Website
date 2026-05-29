using KickZone.Contracts.DTOs.ProductImage;

namespace KickZone.Services.Interfaces;
public interface IProductImageService
{
    Task<ProductImageResponseDto> UploadAsync(CreateProductImageRequestDto request);
    Task SetMainImageAsync(int imageId);
    Task DeleteAsync(int imageId);
    
}