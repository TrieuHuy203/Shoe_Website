using KickZone.DomainEntities.Entities;

namespace KickZone.Repositories.Interfaces;

public interface IInventoryRepository
{
    Task<List<Inventory>> GetAllAsync();
    Task<Inventory?> GetByIdAsync(int id);
    Task<Inventory?> GetByVariantIdAsync(int variantId);

    Task AddAsync(Inventory inventory);
    Task UpdateAsync(Inventory inventory);
    Task SaveChangesAsync();
}