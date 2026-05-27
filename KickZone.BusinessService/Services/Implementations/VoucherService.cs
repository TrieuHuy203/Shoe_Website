
using BCrypt.Net;
using KickZone.Contracts.DTOs.Voucher;
using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using KickZone.Services.Interfaces;


namespace KickZone.Services.Implementations;
public class VoucherService : IVoucherService
{
    private readonly IVoucherRepository _repo;

    public VoucherService(IVoucherRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<VoucherResponseDto>> GetAllAsync()
    {
        var data = await _repo.GetAllAsync();

        return data.Select(x => new VoucherResponseDto
        {
            VoucherId = x.VoucherId,
            Code = x.Code,
            Description = x.Description,
            DiscountType = x.DiscountType,
            DiscountValue = x.DiscountValue,
            Quantity = x.Quantity,
            QuantityUsed = x.QuantityUsed,
            MinOrderValue = x.MinOrderValue,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            IsActive = x.IsActive
        }).ToList();
    }

    public async Task<int> CreateAsync(CreateVoucherRequestDto request)
    {
        // BR01: unique code
        var exist = await _repo.GetByCodeAsync(request.Code);
        if (exist != null)
            throw new Exception("Voucher code already exists");

        // BR02
        if (request.DiscountValue <= 0)
            throw new Exception("Invalid discount amount");

        // BR03
        if (request.DiscountType == "Percent" && request.DiscountValue > 100)
            throw new Exception("Percentage cannot exceed 100%");

        // BR05
        if (request.EndDate <= request.StartDate)
            throw new Exception("Invalid voucher date range");

        var voucher = new Voucher
        {
            Code = request.Code,
            Description = request.Description,
            DiscountType = request.DiscountType,
            DiscountValue = request.DiscountValue,
            Quantity = request.Quantity,
            QuantityUsed = 0,
            MinOrderValue = request.MinOrderValue,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsActive = true,
            IsDeleted = false
        };

        await _repo.AddAsync(voucher);
        await _repo.SaveChangesAsync();
        return voucher.VoucherId;
    }

    public async Task UpdateAsync(int id, UpdateVoucherRequestDto request)
    {
        var voucher = await _repo.GetByIdAsync(id);
        if (voucher == null)
            throw new Exception("Voucher does not exist");

        if (request.EndDate <= request.StartDate)
            throw new Exception("Invalid voucher date range");

        voucher.Code = request.Code;
        voucher.Description = request.Description;
        voucher.DiscountType = request.DiscountType;
        voucher.DiscountValue = request.DiscountValue;
        voucher.Quantity = request.Quantity;
        voucher.MinOrderValue = request.MinOrderValue;
        voucher.StartDate = request.StartDate;
        voucher.EndDate = request.EndDate;

        await _repo.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(int id, bool isActive)
    {
        var voucher = await _repo.GetByIdAsync(id);
        if (voucher == null)
            throw new Exception("Voucher does not exist");

        voucher.IsActive = isActive;

        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var voucher = await _repo.GetByIdAsync(id);
        if (voucher == null)
            throw new Exception("Voucher does not exist");

        voucher.IsDeleted = true;
        voucher.IsActive = false;

        await _repo.SaveChangesAsync();
    }

    public async Task<VoucherResponseDto?> GetByIdAsync(int id)
    {
        var x = await _repo.GetByIdAsync(id);
        if (x == null) return null;

        return new VoucherResponseDto
        {
            VoucherId = x.VoucherId,
            Code = x.Code,
            Description = x.Description,
            DiscountType = x.DiscountType,
            DiscountValue = x.DiscountValue,
            Quantity = x.Quantity,
            QuantityUsed = x.QuantityUsed,
            MinOrderValue = x.MinOrderValue,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            IsActive = x.IsActive
        };
    }
    public async Task<bool> ExistsAsync(int id)
    {
        return await _repo.ExistsAsync(id);
    }

// apply voucher for order 
   public async Task<ApplyVoucherResponseDto> ApplyVoucherAsync(ApplyVoucherRequestDto request)
{
    var voucher = await _repo.GetByCodeAsync(request.Code);

    // BR01: không tìm thấy voucher
    if (voucher == null)
    {
        return new ApplyVoucherResponseDto
        {
            IsValid = false,
            Message = "Voucher does not exist"
        };
    }

    // BR07: inactive
    if (voucher.IsActive != true)
    {
        return new ApplyVoucherResponseDto
        {
            IsValid = false,
            Message = "Voucher is not active"
        };
    }

    // BR06: expired
    if (voucher.StartDate.HasValue && voucher.StartDate > DateTime.Now ||
        voucher.EndDate.HasValue && voucher.EndDate < DateTime.Now)
    {
        return new ApplyVoucherResponseDto
        {
            IsValid = false,
            Message = "Voucher has expired"
        };
    }

    // BR09: min order value
    if (voucher.MinOrderValue.HasValue &&
        request.OrderAmount < voucher.MinOrderValue)
    {
        return new ApplyVoucherResponseDto
        {
            IsValid = false,
            Message = "Order does not meet minimum value"
        };
    }

    decimal discount = 0;

    // tính discount
    if (voucher.DiscountType == "Percent")
    {
        discount = request.OrderAmount * voucher.DiscountValue / 100;

        // BR03: max 100%
        if (voucher.DiscountValue > 100)
        {
            return new ApplyVoucherResponseDto
            {
                IsValid = false,
                Message = "Invalid discount percentage"
            };
        }
    }
    else // Fixed
    {
        discount = voucher.DiscountValue;
    }

    // không cho giảm quá số tiền đơn hàng
    if (discount > request.OrderAmount)
        discount = request.OrderAmount;

    return new ApplyVoucherResponseDto
    {
        IsValid = true,
        Message = "Voucher applied successfully",
        DiscountAmount = discount,
        FinalAmount = request.OrderAmount - discount
    };
}

}