namespace KickZone.Contracts.DTOs.ProductVariantDTOs;

public class CreateProductVariantRequestDto
{
    public int ProductId { get; set; }

    public string? Size { get; set; }
    public string? Color { get; set; }
    public string? Type { get; set; }

    public string Sku { get; set; } = null!;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string? Image { get; set; }

    public bool IsActive { get; set; } = true;
}