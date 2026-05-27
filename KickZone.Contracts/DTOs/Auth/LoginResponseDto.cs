namespace KickZone.Contracts.DTOs.Auth;

public class LoginResponseDto
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;
}