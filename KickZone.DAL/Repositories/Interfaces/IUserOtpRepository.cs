using KickZone.DomainEntities.Entities;

namespace KickZone.Repositories.Interfaces;

public interface IUserOtpRepository
{
    Task CreateAsync(UserOTP otp); // Tạo mới OTP cho người dùng

    Task<UserOTP?> GetValidOtpAsync(int userId, string otpCode); // Lấy OTP hợp lệ (chưa hết hạn, chưa sử dụng) cho người dùng
    Task UpdateAsync(UserOTP otp);
}