using KickZone.Contracts.DTOs.Auth;

namespace KickZone.Services.Interfaces;

public interface IAuthService
{
    Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request); // Đăng ký người dùng mới
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request); // Đăng nhập người dùng
}