namespace KickZone.Contracts.DTOs.InventoryDTOs;

public class InventoryDto
{
    public int InventoryId { get; set; }
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public string Status { get; set; } = default!;
    public DateTime? UpdatedAt { get; set; }
}