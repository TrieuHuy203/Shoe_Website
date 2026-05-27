using BCrypt.Net;
using KickZone.Contracts.DTOs.UserDTOs;
using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using KickZone.Services.Interfaces;


namespace KickZone.Services.Implementations;



public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserResponseDto>> GetUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();

        return users.Select(x => new UserResponseDto
        {
            UserId = x.UserId,
            Username = x.Username,
            Email = x.Email,
            FullName = x.FullName,
            Phone = x.Phone,
            Gender = x.Gender,
            IsActive = x.IsActive,
            IsVerified = x.IsVerified,
            CreatedAt = x.CreatedAt
        }).ToList();
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found");

        return new UserResponseDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            FullName = user.FullName,
            Phone = user.Phone,
            Gender = user.Gender,
            DateOfBirth = user.DateOfBirth,
            IsActive = user.IsActive,
            IsVerified = user.IsVerified,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<UserResponseDto> CreateUserAsync(CreateUserRequestDto request)
    {
        var emailExist = await _userRepository.GetByEmailAsync(request.Email);

        if (emailExist != null)
            throw new Exception("Email already exists");

        var usernameExist = await _userRepository.GetByUsernameAsync(request.Username);

        if (usernameExist != null)
            throw new Exception("Username already exists");

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            FullName = request.FullName,
            Phone = request.Phone,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth,
            IsActive = true,
            IsDeleted = false,
            IsVerified = true, // 👈 thêm dòng này

            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.CreateAsync(user);
        await _userRepository.SaveChangesAsync();

        return new UserResponseDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            FullName = user.FullName,
            Phone = user.Phone,
            Gender = user.Gender,
            DateOfBirth = user.DateOfBirth,
            IsActive = user.IsActive,
            IsVerified = user.IsVerified,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<UserResponseDto> UpdateUserAsync(int userId, UpdateUserRequestDto request)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found");

        user.FullName = request.FullName;
        user.Phone = request.Phone;
        user.Gender = request.Gender;
        user.DateOfBirth = request.DateOfBirth;
        user.IsActive = request.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();

        return new UserResponseDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email
        };
    }

    public async Task LockUserAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found");

        user.IsActive = false;

        await _userRepository.SaveChangesAsync();
    }

    public async Task UnlockUserAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found");

        user.IsActive = true;

        await _userRepository.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(int userId, bool isActive)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found");

        user.IsActive = isActive;

        await _userRepository.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found");

        user.IsDeleted = true; // Đánh dấu là đã xóa, và khi xoá thì IsActive sẽ thành false là khong hoạt động

        user.IsActive = false;

        await _userRepository.SaveChangesAsync();
    }
}