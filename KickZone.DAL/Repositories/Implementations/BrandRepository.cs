using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using KickZone.DAL.Entities;

namespace KickZone.Repositories.Implementations;

public class BrandRepository : IBrandRepository
{
    private readonly ShoeShopDbContext _context;

    public BrandRepository(ShoeShopDbContext context)
    {
        _context = context;
    }

    public async Task<List<Brand>> GetAllAsync()
    {
        return await _context.Brands
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<Brand?> GetByIdAsync(int id)
    {
        return await _context.Brands
            .FirstOrDefaultAsync(x =>
                x.BrandId == id &&
                !x.IsDeleted);
    }

    public async Task<Brand?> GetByNameAsync(string name)
    {
        return await _context.Brands
            .FirstOrDefaultAsync(x =>
                x.Name == name &&
                !x.IsDeleted);
    }

    public async Task AddAsync(Brand brand)
    {
        await _context.Brands.AddAsync(brand);
    }

    public Task UpdateAsync(Brand brand)
    {
        _context.Brands.Update(brand);

        return Task.CompletedTask;
    }

    public async Task<bool> HasProductsAsync(int brandId)
    {
        return await _context.Products
            .AnyAsync(x => x.BrandId == brandId);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}