using KickZone.DAL.Entities;
using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KickZone.Repositories.Implementations;
public class CategoryRepository : ICategoryRepository
{
    private readonly ShoeShopDbContext _context;

    public CategoryRepository(ShoeShopDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories
            .Include(x => x.Parent)
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .Include(x => x.Parent)
            .FirstOrDefaultAsync(x => x.CategoryId == id);
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
    }

    public void Update(Category category)
    {
        _context.Categories.Update(category);
    }

    public void Delete(Category category)
    {
        _context.Categories.Remove(category);
    }

    public async Task<bool> HasProductsAsync(int categoryId)
    {
        return await _context.Products.AnyAsync(x => x.CategoryId == categoryId);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Categories.AnyAsync(x => x.CategoryId == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}