public class ApplyVoucherResponseDto
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = null!;
    public decimal DiscountAmount { get; set; }
    public decimal FinalAmount { get; set; }
}