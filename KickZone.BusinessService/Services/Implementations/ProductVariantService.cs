using KickZone.Contracts.DTOs.ProductVariantDTOs;
using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;

public class ProductVariantService : IProductVariantService
{
    private readonly IProductVariantRepository _repo;
    private readonly IProductRepository _productRepo;

    public ProductVariantService(
        IProductVariantRepository repo,
        IProductRepository productRepo)
    {
        _repo = repo;
        _productRepo = productRepo;
    }

    public async Task<List<ProductVariantResponseDto>> GetAllAsync()
    {
        var data = await _repo.GetAllAsync();

        return data.Select(x => new ProductVariantResponseDto
        {
            VariantId = x.VariantId,
            ProductId = x.ProductId,
            Size = x.Size,
            Color = x.Color,
            Type = x.Type,
            Sku = x.Sku!,
            Price = x.Price ?? 0,
            StockQuantity = x.Inventory?.Quantity ?? 0,
            IsActive = x.IsActive ?? true
        }).ToList();
    }

    public async Task<ProductVariantResponseDto?> GetByIdAsync(int id)
    {
        var x = await _repo.GetByIdAsync(id);
        if (x == null) return null;

        return new ProductVariantResponseDto
        {
            VariantId = x.VariantId,
            ProductId = x.ProductId,
            Size = x.Size,
            Color = x.Color,
            Type = x.Type,
            Sku = x.Sku!,
            Price = x.Price ?? 0,
            StockQuantity = x.Inventory?.Quantity ?? 0,
            IsActive = x.IsActive ?? true
        };
    }

    public async Task CreateAsync(CreateProductVariantRequestDto dto)
    {
        // BR01: product must exist
        var product = await _productRepo.GetByIdAsync(dto.ProductId);
        if (product == null)
            throw new Exception("Product does not exist");

        // BR02: SKU unique
        var skuExists = await _repo.GetBySkuAsync(dto.Sku);
        if (skuExists != null)
            throw new Exception("Variant SKU already exists");

        // BR03
        if (dto.Price <= 0)
            throw new Exception("Variant price must be greater than 0");

        // BR04
        if (dto.StockQuantity < 0)
            throw new Exception("Stock quantity cannot be negative");

        // BR05
        var duplicate = await _repo.ExistsDuplicateAttributesAsync(
            dto.ProductId, dto.Size, dto.Color, dto.Type);

        if (duplicate)
            throw new Exception("Variant already exists");

        var entity = new ProductVariant
        {
            ProductId = dto.ProductId,
            Size = dto.Size,
            Color = dto.Color,
            Type = dto.Type,
            Sku = dto.Sku,
            Price = dto.Price,
            IsActive = dto.IsActive,
            IsDeleted = false
        };

        await _repo.AddAsync(entity);
    }

    public async Task UpdateAsync(int id, UpdateProductVariantRequestDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            throw new Exception("Product variant does not exist");

        if (dto.Price <= 0)
            throw new Exception("Variant price must be greater than 0");

        if (dto.StockQuantity < 0)
            throw new Exception("Stock quantity cannot be negative");

        entity.Size = dto.Size;
        entity.Color = dto.Color;
        entity.Type = dto.Type;
        entity.Sku = dto.Sku;
        entity.Price = dto.Price;
        entity.IsActive = dto.IsActive;

        await _repo.UpdateAsync(entity);
    }

    public async Task ChangeStatusAsync(int id, ChangeVariantStatusDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            throw new Exception("Product variant does not exist");

        entity.IsActive = dto.IsActive;

        await _repo.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            throw new Exception("Product variant does not exist");

        await _repo.DeleteAsync(entity);
    }
}