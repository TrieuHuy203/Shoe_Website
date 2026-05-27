using KickZone.Contracts.DTOs.Category;

namespace KickZone.Services.Interfaces;
public interface ICategoryService
{
    Task<List<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto?> GetByIdAsync(int id);

    Task CreateAsync(CreateCategoryRequestDto request);
    Task UpdateAsync(int id, UpdateCategoryRequestDto request);

    Task ChangeStatusAsync(int id);
    Task DeleteAsync(int id);
}