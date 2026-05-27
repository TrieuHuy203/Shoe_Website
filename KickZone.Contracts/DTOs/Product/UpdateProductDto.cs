namespace KickZone.Contracts.DTOs.ProductDTOs;

public class UpdateProductDto
{
    public string Name { get; set; } = null!;

    public string SKU { get; set; } = null!;

    public string? Description { get; set; }

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public decimal Price { get; set; }

    public decimal? SalePrice { get; set; }

    //public int StockQuantity { get; set; }

    //public string ?Status { get; set; } = "Active";
}