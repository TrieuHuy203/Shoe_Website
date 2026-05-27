
using KickZone.Contracts.DTOs.Voucher;
namespace KickZone.Services.Interfaces;


public interface IVoucherService
{
    Task<List<VoucherResponseDto>> GetAllAsync();
    Task<VoucherResponseDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateVoucherRequestDto request);
    Task UpdateAsync(int id, UpdateVoucherRequestDto request);
    Task<bool> ExistsAsync(int id);
    Task ChangeStatusAsync(int id, bool isActive);
    Task DeleteAsync(int id);
    // apply voucher for order 
    Task<ApplyVoucherResponseDto> ApplyVoucherAsync(ApplyVoucherRequestDto request);
}