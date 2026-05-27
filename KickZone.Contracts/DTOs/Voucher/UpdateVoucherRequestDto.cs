namespace KickZone.Contracts.DTOs.Voucher;
public class UpdateVoucherRequestDto
{
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
    public string DiscountType { get; set; } = null!;
    public decimal DiscountValue { get; set; }
    public int? Quantity { get; set; }
    public decimal? MinOrderValue { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}