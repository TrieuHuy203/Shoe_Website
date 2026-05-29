    namespace KickZone.Contracts.DTOs.OTP;

    public class VerifyOtpRequestDto
    {
        public string Email { get; set; } = null!; 

        public string OTPCode { get; set; } = null!;
    }   