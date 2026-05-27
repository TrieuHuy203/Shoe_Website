using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using KickZone.DAL.Entities;

namespace KickZone.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly ShoeShopDbContext _context;

    public ProductRepository(ShoeShopDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(x => x.Category)
            .Include(x => x.Brand)
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(x => x.Category)
            .Include(x => x.Brand)
            .FirstOrDefaultAsync(x => x.ProductId == id && !x.IsDeleted);
    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await _context.Products
            .FirstOrDefaultAsync(x => x.SKU == sku && !x.IsDeleted);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}