// trả về thông tin người dùng sau khi đăng ký thành công, có thể bao gồm UserId, Username, Email và một thông điệp xác nhận
namespace KickZone.Contracts.DTOs.Auth;

public class RegisterResponseDto
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Message { get; set; } = null!; // Thông điệp xác nhận đăng ký thành công
}