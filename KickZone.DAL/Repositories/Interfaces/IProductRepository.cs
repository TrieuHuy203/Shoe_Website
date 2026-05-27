using KickZone.DomainEntities.Entities;

namespace KickZone.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(int id);

    Task<Product?> GetBySkuAsync(string sku);

    Task AddAsync(Product product);

    void Update(Product product);

    Task SaveChangesAsync();
}