using KickZone.Contracts.DTOs.ProductDTOs;
using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using KickZone.Services.Interfaces;

namespace KickZone.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBrandRepository _brandRepository;

    public ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IBrandRepository brandRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _brandRepository = brandRepository;
    }

    public async Task<List<ProductResponseDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return products.Select(x => new ProductResponseDto
        {
            ProductId = x.ProductId,
            Name = x.Name,
            SKU = x.SKU,
            Description = x.Description,
            Price = x.Price,
            SalePrice = x.SalePrice,
           // StockQuantity = x.StockQuantity,
           // Status = x.Status,
            CategoryName = x.Category.Name,
            BrandName = x.Brand.Name,
            IsActive = x.IsActive,
            CreatedAt = x.CreatedAt
        }).ToList();
    }

    public async Task<ProductResponseDto?> GetByIdAsync(int id)
    {
        var x = await _productRepository.GetByIdAsync(id);

        if (x == null)
            throw new Exception("Product does not exist");

        return new ProductResponseDto
        {
            ProductId = x.ProductId,
            Name = x.Name,
            SKU = x.SKU,
            Description = x.Description,
            Price = x.Price,
            SalePrice = x.SalePrice,
           // StockQuantity = x.StockQuantity,
           //Status = x.Status,
            CategoryName = x.Category.Name,
            BrandName = x.Brand.Name,
            IsActive = x.IsActive,
            CreatedAt = x.CreatedAt
        };
    }

    public async Task CreateAsync(CreateProductDto dto)
    {
        var existingSku = await _productRepository.GetBySkuAsync(dto.SKU);

        if (existingSku != null)
            throw new Exception("Product SKU already exists");

        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

        if (category == null)
            throw new Exception("Category does not exist");

        var brand = await _brandRepository.GetByIdAsync(dto.BrandId);

        if (brand == null)
            throw new Exception("Brand does not exist");

        if (dto.Price <= 0)
            throw new Exception("Product price must be greater than 0");

        // if (dto.StockQuantity < 0)
        //     throw new Exception("Stock quantity cannot be negative");

        var product = new Product
        {
            Name = dto.Name,
            SKU = dto.SKU,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            BrandId = dto.BrandId,
            Price = dto.Price,
            SalePrice = dto.SalePrice,
           // StockQuantity = dto.StockQuantity,
           // Status = dto.Status,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await _productRepository.AddAsync(product);

        await _productRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            throw new Exception("Product does not exist");

        product.Name = dto.Name;
        product.SKU = dto.SKU;
        product.Description = dto.Description;
        product.CategoryId = dto.CategoryId;
        product.BrandId = dto.BrandId;
        product.Price = dto.Price;
        product.SalePrice = dto.SalePrice;
        //product.StockQuantity = dto.StockQuantity;
       // product.Status = dto.Status;
        product.UpdatedAt = DateTime.UtcNow;

        _productRepository.Update(product);

        await _productRepository.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(int id, ChangeProductStatusDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            throw new Exception("Product does not exist");

        // product.Status = dto.Status;

        product.IsActive = dto.IsActive;

        _productRepository.Update(product);

        await _productRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            throw new Exception("Product does not exist");

        product.IsDeleted = true;

        product.IsActive = false;

        _productRepository.Update(product);

        await _productRepository.SaveChangesAsync();
    }
}