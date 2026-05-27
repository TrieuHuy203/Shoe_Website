using KickZone.Contracts.DTOs.BrandDTOs;
using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using KickZone.Services.Interfaces;

namespace KickZone.Services.Implementations;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<List<BrandResponseDto>> GetAllAsync()
    {
        var brands = await _brandRepository.GetAllAsync();

        return brands.Select(x => new BrandResponseDto
        {
            BrandId = x.BrandId,
            Name = x.Name,
            Description = x.Description,
            Logo = x.Logo,
            IsActive = x.IsActive,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        }).ToList();
    }

    public async Task<BrandResponseDto> GetByIdAsync(int id)
    {
        var brand = await _brandRepository.GetByIdAsync(id);

        if (brand == null)
            throw new Exception("Brand does not exist");

        return new BrandResponseDto
        {
            BrandId = brand.BrandId,
            Name = brand.Name,
            Description = brand.Description,
            Logo = brand.Logo,
            IsActive = brand.IsActive,
            CreatedAt = brand.CreatedAt,
            UpdatedAt = brand.UpdatedAt
        };
    }

    public async Task CreateAsync(CreateBrandDto request)
    {
        var existingBrand = await _brandRepository
            .GetByNameAsync(request.Name);

        if (existingBrand != null)
            throw new Exception("Brand name already exists");

        var brand = new Brand
        {
            Name = request.Name,
            Description = request.Description,
            Logo = request.Logo,

            IsActive = true,
            IsDeleted = false,

            CreatedAt = DateTime.UtcNow
        };

        await _brandRepository.AddAsync(brand);

        await _brandRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UpdateBrandDto request)
    {
        var brand = await _brandRepository.GetByIdAsync(id);

        if (brand == null)
            throw new Exception("Brand does not exist");

        var duplicateBrand = await _brandRepository
            .GetByNameAsync(request.Name);

        if (duplicateBrand != null &&
            duplicateBrand.BrandId != id)
        {
            throw new Exception("Brand name already exists");
        }

        brand.Name = request.Name;
        if (request.Description != null)
{
    brand.Description = request.Description;
}

if (request.Logo != null)
{
    brand.Logo = request.Logo;
}


        if (request.IsActive != null)
{
   brand.IsActive = request.IsActive ?? brand.IsActive;
}

        brand.UpdatedAt = DateTime.UtcNow;

        await _brandRepository.UpdateAsync(brand);

        await _brandRepository.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(int id, bool isActive)
    {
        var brand = await _brandRepository.GetByIdAsync(id);

        if (brand == null)
            throw new Exception("Brand does not exist");

        brand.IsActive = isActive;

        brand.UpdatedAt = DateTime.UtcNow;

        await _brandRepository.UpdateAsync(brand);

        await _brandRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var brand = await _brandRepository.GetByIdAsync(id);

        if (brand == null)
            throw new Exception("Brand does not exist");

        var hasProducts = await _brandRepository
            .HasProductsAsync(id);

        if (hasProducts)
        {
            throw new Exception(
                "Cannot delete brand with existing products");
        }

        brand.IsDeleted = true;

        brand.UpdatedAt = DateTime.UtcNow;

        await _brandRepository.UpdateAsync(brand);

        await _brandRepository.SaveChangesAsync();
    }
}