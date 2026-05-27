using KickZone.Contracts.DTOs.ProductDTOs;

namespace KickZone.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductResponseDto>> GetAllAsync();

    Task<ProductResponseDto?> GetByIdAsync(int id);

    Task CreateAsync(CreateProductDto dto);

    Task UpdateAsync(int id, UpdateProductDto dto);

   Task ChangeStatusAsync(int id, ChangeProductStatusDto dto);

    Task DeleteAsync(int id);
}