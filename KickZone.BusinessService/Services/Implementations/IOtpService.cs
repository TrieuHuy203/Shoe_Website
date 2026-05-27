using KickZone.Contracts.DTOs.OTP;
using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using KickZone.Services.Interfaces;

namespace KickZone.Services.Implementations;


using Microsoft.Extensions.Logging;

public class OtpService : IOtpService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserOtpRepository _otpRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<OtpService> _logger;

    public OtpService(
        IUserRepository userRepository,
        IUserOtpRepository otpRepository,
        IEmailService emailService,
        ILogger<OtpService> logger)
    {
        _userRepository = userRepository;
        _otpRepository = otpRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task SendOtpAsync(string email)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("User not found for email: {Email}", email);
                throw new Exception("User not found");
            }

            var otpCode = new Random()
                .Next(100000, 999999)
                .ToString();

            var otp = new UserOTP
            {
               UserID = user.UserId,
    OTPCode = otpCode,
    IsUsed = false,
    CreatedAt = DateTime.Now,
    ExpiredAt = DateTime.Now.AddMinutes(5)
            };

            await _otpRepository.CreateAsync(otp);

            await _emailService.SendOtpEmailAsync(
                user.Email,
                otpCode
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi khi gửi OTP cho email: {Email}", email);
            throw;
        }
    }

    public async Task VerifyOtpAsync(
        VerifyOtpRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        var otp = await _otpRepository.GetValidOtpAsync(
            user.UserId,
            request.OTPCode
        );

        if (otp == null)
        {
            throw new Exception("OTP invalid or expired");
        }

        otp.IsUsed = true;

        user.IsVerified = true;
        user.IsActive = true;
        

        await _otpRepository.UpdateAsync(otp);

        await _userRepository.UpdateAsync(user);
    }
}