using KickZone.Contracts.DTOs.ProductVariantDTOs;

public interface IProductVariantService
{
    Task<List<ProductVariantResponseDto>> GetAllAsync();
    Task<ProductVariantResponseDto?> GetByIdAsync(int id);

    Task CreateAsync(CreateProductVariantRequestDto dto);
    Task UpdateAsync(int id, UpdateProductVariantRequestDto dto);

    Task ChangeStatusAsync(int id, ChangeVariantStatusDto dto);

    Task DeleteAsync(int id);
}