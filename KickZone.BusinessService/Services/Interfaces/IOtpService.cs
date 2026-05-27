using KickZone.Contracts.DTOs.OTP;

namespace KickZone.Services.Interfaces;

public interface IOtpService
{
   Task SendOtpAsync(string email);

    Task VerifyOtpAsync(VerifyOtpRequestDto request);
}