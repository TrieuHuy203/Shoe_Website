using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using KickZone.DAL.Entities;

namespace KickZone.Repositories.Implementations;
public class ProductImageRepository : IProductImageRepository
{
    private readonly ShoeShopDbContext _context;

    public ProductImageRepository(ShoeShopDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ProductImage image)
    {
        await _context.ProductImages.AddAsync(image);
    }

    public async Task<List<ProductImage>> GetByProductIdAsync(int productId)
    {
        return await _context.ProductImages
            .Where(x => x.ProductId == productId && !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<ProductImage?> GetByIdAsync(int id)
    {
        return await _context.ProductImages
            .FirstOrDefaultAsync(x => x.ImageId == id && !x.IsDeleted);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}