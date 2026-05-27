using KickZone.DomainEntities.Entities;

namespace KickZone.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email); // Lấy người dùng theo email

    Task<User?> GetByUsernameAsync(string username); // Lấy người dùng theo username

    Task CreateAsync(User user); // Tạo mới người dùng
    Task UpdateAsync(User user); // Cập nhật thông tin người dùng
    
////////////////// user management //////////////////
   Task<List<User>> GetAllUsersAsync(); // Lấy tất cả người dùng

    Task<User?> GetUserByIdAsync(int userId); // Lấy người dùng theo ID


    //Task DeleteUserAsync(User user); // Xóa người dùng (có thể là xóa mềm hoặc xóa cứng tùy vào cách bạn thiết kế)

    Task SaveChangesAsync(); // Lưu thay đổi vào database


}