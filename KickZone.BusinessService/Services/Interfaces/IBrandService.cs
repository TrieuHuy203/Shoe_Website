using KickZone.Contracts.DTOs.BrandDTOs;

namespace KickZone.Services.Interfaces;

public interface IBrandService
{
    Task<List<BrandResponseDto>> GetAllAsync();

    Task<BrandResponseDto> GetByIdAsync(int id);

    Task CreateAsync(CreateBrandDto request);

    Task UpdateAsync(int id, UpdateBrandDto request);

    Task ChangeStatusAsync(int id, bool isActive);

    Task DeleteAsync(int id);
}