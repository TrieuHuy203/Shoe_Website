// token sau khi dang nhập thành công
using KickZone.DomainEntities.Entities;

namespace KickZone.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user); // Phương thức để tạo token JWT dựa trên thông tin người dùng
}