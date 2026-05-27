using KickZone.DomainEntities.Entities;

namespace KickZone.Repositories.Interfaces;

public interface IBrandRepository
{
    Task<List<Brand>> GetAllAsync();

    Task<Brand?> GetByIdAsync(int id);

    Task<Brand?> GetByNameAsync(string name);

    Task AddAsync(Brand brand);

    Task UpdateAsync(Brand brand);

    Task<bool> HasProductsAsync(int brandId);

    Task SaveChangesAsync();
}