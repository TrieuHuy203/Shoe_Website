using KickZone.DomainEntities.Entities;

public interface IProductVariantRepository
{
    Task<List<ProductVariant>> GetAllAsync();
    Task<ProductVariant?> GetByIdAsync(int id);

    Task<ProductVariant?> GetBySkuAsync(string sku);

    Task AddAsync(ProductVariant entity);
    Task UpdateAsync(ProductVariant entity);
    Task DeleteAsync(ProductVariant entity);

    Task<bool> ExistsDuplicateAttributesAsync(int productId, string? size, string? color, string? type);
}