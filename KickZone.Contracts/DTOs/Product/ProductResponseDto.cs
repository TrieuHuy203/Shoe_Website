namespace KickZone.Contracts.DTOs.ProductDTOs;

public class ProductResponseDto
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string SKU { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public decimal? SalePrice { get; set; }

   // public int StockQuantity { get; set; }

    //public string Status { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string BrandName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
}