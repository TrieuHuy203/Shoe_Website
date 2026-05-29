using KickZone.Contracts.DTOs.ProductImage;
using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using KickZone.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace KickZone.Services.Implementations;
public class ProductImageService : IProductImageService
{
    private readonly IProductImageRepository _repo;
    private readonly IWebHostEnvironment _env;

    public ProductImageService(
        IProductImageRepository repo,
        IWebHostEnvironment env)
    {
        _repo = repo;
        _env = env;
    }

    public async Task<ProductImageResponseDto> UploadAsync(CreateProductImageRequestDto request)
    {
        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/products");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var fileName = Guid.NewGuid() + Path.GetExtension(request.File.FileName);
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream);
        }

        var images = await _repo.GetByProductIdAsync(request.ProductId);
        var maxSort = images.Any() ? images.Max(x => x.SortOrder) : 0;

        var image = new ProductImage
        {
            ProductId = request.ProductId,
            ImageUrl = $"/uploads/products/{fileName}",
            IsMain = request.IsMain,
            SortOrder = maxSort + 1,
            CreatedAt = DateTime.Now,
            IsDeleted = false
        };

        // nếu set main → reset ảnh khác
        if (request.IsMain)
        {
            foreach (var img in images)
                img.IsMain = false;
        }

        await _repo.AddAsync(image);
        await _repo.SaveChangesAsync();

        return new ProductImageResponseDto
        {
            ImageId = image.ImageId,
            ProductId = image.ProductId,
            ImageUrl = image.ImageUrl,
            IsMain = image.IsMain ?? false,
            SortOrder = image.SortOrder
        };
    }

    public async Task SetMainImageAsync(int imageId)
    {
        var image = await _repo.GetByIdAsync(imageId);
        if (image == null) throw new Exception("Image not found");

        var images = await _repo.GetByProductIdAsync(image.ProductId);

        foreach (var img in images)
            img.IsMain = false;

        image.IsMain = true;

        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int imageId)
    {
        var image = await _repo.GetByIdAsync(imageId);
        if (image == null) throw new Exception("Image not found");

        image.IsDeleted = true;

        await _repo.SaveChangesAsync();
    }
}