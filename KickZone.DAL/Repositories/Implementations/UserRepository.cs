using KickZone.DAL.Entities; // Đảm bảo bạn đã thêm using cho User entity
using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;



namespace KickZone.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly ShoeShopDbContext _context;

    public UserRepository(ShoeShopDbContext context)
    {
        _context = context;
    }

    // Lấy người dùng theo email
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted); // Chỉ lấy người dùng chưa bị xóa mềm
    }
// Lấy người dùng theo username
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Username == username && !x.IsDeleted);
    }
// Tạo mới người dùng
    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user); // Thêm người dùng mới vào DbSet

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
{
    _context.Users.Update(user);

    await _context.SaveChangesAsync();
}

// Lấy tất cả người dùng
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }


// lấy người dùng theo ID
    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                !x.IsDeleted);
    }
  

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}