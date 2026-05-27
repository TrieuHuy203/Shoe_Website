using KickZone.DomainEntities.Entities;

namespace KickZone.Repositories.Interfaces;
public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category?> GetByNameAsync(string name);

    Task AddAsync(Category category);
    void Update(Category category);
    void Delete(Category category);

    Task<bool> HasProductsAsync(int categoryId);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}