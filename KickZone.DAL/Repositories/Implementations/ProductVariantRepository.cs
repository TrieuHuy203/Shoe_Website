using KickZone.DAL.Entities;
using KickZone.DomainEntities.Entities;
using Microsoft.EntityFrameworkCore;

public class ProductVariantRepository : IProductVariantRepository
{
    private readonly ShoeShopDbContext _context;

    public ProductVariantRepository(ShoeShopDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductVariant>> GetAllAsync()
    {
        return await _context.ProductVariants
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<ProductVariant?> GetByIdAsync(int id)
    {
        return await _context.ProductVariants
            .FirstOrDefaultAsync(x => x.VariantId == id && !x.IsDeleted);
    }

    public async Task<ProductVariant?> GetBySkuAsync(string sku)
    {
        return await _context.ProductVariants
            .FirstOrDefaultAsync(x => x.Sku == sku && !x.IsDeleted);
    }

    public async Task AddAsync(ProductVariant entity)
    {
        await _context.ProductVariants.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductVariant entity)
    {
        _context.ProductVariants.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ProductVariant entity)
    {
        entity.IsDeleted = true;
        _context.ProductVariants.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsDuplicateAttributesAsync(int productId, string? size, string? color, string? type)
    {
        return await _context.ProductVariants.AnyAsync(x =>
            x.ProductId == productId &&
            x.Size == size &&
            x.Color == color &&
            x.Type == type &&
            !x.IsDeleted);
    }
//     public async Task SaveChangesAsync()
// {
//     await _context.SaveChangesAsync();
// }
}