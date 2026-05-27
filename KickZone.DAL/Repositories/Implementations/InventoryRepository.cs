using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using KickZone.DAL.Entities;

namespace KickZone.Repositories.Implementations;

public class InventoryRepository : IInventoryRepository
{
    private readonly ShoeShopDbContext _context;

    public InventoryRepository(ShoeShopDbContext context)
    {
        _context = context;
    }

    public async Task<List<Inventory>> GetAllAsync()
    {
        return await _context.Inventories
            .Include(x => x.ProductVariant)
            .ToListAsync();
    }

    public async Task<Inventory?> GetByIdAsync(int id)
    {
        return await _context.Inventories
            .Include(x => x.ProductVariant)
            .FirstOrDefaultAsync(x => x.InventoryId == id);
    }

    public async Task<Inventory?> GetByVariantIdAsync(int variantId)
    {
        return await _context.Inventories
            .FirstOrDefaultAsync(x => x.ProductVariantId == variantId);
    }

    public async Task AddAsync(Inventory inventory)
    {
        await _context.Inventories.AddAsync(inventory);
    }

    public async Task UpdateAsync(Inventory inventory)
    {
        _context.Inventories.Update(inventory);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}