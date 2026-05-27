using BCrypt.Net;
using KickZone.Contracts.DTOs.Category;
using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using KickZone.Services.Interfaces;

namespace KickZone.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;

    public CategoryService(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        var data = await _repo.GetAllAsync();

        return data.Select(x => new CategoryResponseDto
        {
            CategoryId = x.CategoryId,
            Name = x.Name,
            Description = x.Description,
            ParentId = x.ParentId,
            IsDeleted = x.IsDeleted,
            ParentName = x.Parent?.Name
        }).ToList();
    }

    public async Task<CategoryResponseDto?> GetByIdAsync(int id)
    {
        var x = await _repo.GetByIdAsync(id);
        if (x == null) return null;

        return new CategoryResponseDto
        {
            CategoryId = x.CategoryId,
            Name = x.Name,
            Description = x.Description,
            ParentId = x.ParentId,
            IsDeleted = x.IsDeleted,
            ParentName = x.Parent?.Name
        };
    }

    public async Task CreateAsync(CreateCategoryRequestDto request)
    {
        // BR02
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new Exception("Category name is required");

        // BR01
        var exists = await _repo.GetByNameAsync(request.Name);
        if (exists != null)
            throw new Exception("Category name must be unique");

        // BR04
        if (request.ParentId.HasValue)
        {
            var parentExists = await _repo.ExistsAsync(request.ParentId.Value);
            if (!parentExists)
                throw new Exception("Parent category does not exist");
        }

        var category = new Category
        {
            Name = request.Name,
            Description = request.Description,
            ParentId = request.ParentId,
            IsDeleted = false
        };

        await _repo.AddAsync(category);
        await _repo.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UpdateCategoryRequestDto request)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category == null)
            throw new Exception("Category not found");

        var duplicate = await _repo.GetByNameAsync(request.Name);
        if (duplicate != null && duplicate.CategoryId != id)
            throw new Exception("Category name must be unique");

        if (request.ParentId.HasValue)
        {
            var parentExists = await _repo.ExistsAsync(request.ParentId.Value);
            if (!parentExists)
                throw new Exception("Parent category does not exist");
        }

        category.Name = request.Name;
        category.Description = request.Description;
        category.ParentId = request.ParentId;

        _repo.Update(category);
        await _repo.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category == null)
            throw new Exception("Category not found");

        category.IsActive = !category.IsActive;

        _repo.Update(category);
        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category == null)
            throw new Exception("Category not found");

        // BR06: có sản phẩm thì không xóa cứng
        var hasProducts = await _repo.HasProductsAsync(id);

        if (hasProducts)
        {
            // soft delete
            category.IsDeleted = true;
            _repo.Update(category);
        }
        else
        {
            // hard delete
            _repo.Delete(category);
        }

        await _repo.SaveChangesAsync();
    }
}