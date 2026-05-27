using KickZone.Contracts.DTOs.OTP;
using KickZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KickZone.Controllers.Auth;

[ApiController]
[Route("api/auth/otp")]
public class OtpController : ControllerBase
{
    private readonly IOtpService _otpService;

    public OtpController(IOtpService otpService)
    {
        _otpService = otpService;
    }

    // ================= VERIFY OTP =================
    [HttpPost("verify")]
    public async Task<IActionResult> VerifyOtp(
        [FromBody] VerifyOtpRequestDto request
    )
    {
        try
        {
            await _otpService.VerifyOtpAsync(request);

            return Ok(new
            {
                success = true,
                message = "OTP verified successfully"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                message = ex.Message
            });
        }
    }

    // ================= RESEND OTP =================
    [HttpPost("resend")] // gửi lại OTP
    public async Task<IActionResult> ResendOtp(
        [FromBody] VerifyOtpRequestDto request
    )
    {
        try
        {
            await _otpService.SendOtpAsync(request.Email);

            return Ok(new
            {
                success = true,
                message = "OTP resent successfully"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                message = ex.Message
            });
        }
    }
}