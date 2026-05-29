using KickZone.DomainEntities.Entities;

namespace KickZone.Repositories.Interfaces;
public interface IProductImageRepository
{
    Task AddAsync(ProductImage image);
    Task<List<ProductImage>> GetByProductIdAsync(int productId);
    Task<ProductImage?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}