namespace KickZone.Services.Interfaces;

using KickZone.Contracts.DTOs.UserDTOs;

public interface IUserService
{
    Task<List<UserResponseDto>> GetUsersAsync();

    Task<UserResponseDto?> GetUserByIdAsync(int userId);

    Task<UserResponseDto> CreateUserAsync(CreateUserRequestDto request);

    Task<UserResponseDto> UpdateUserAsync(int userId, UpdateUserRequestDto request);

    Task LockUserAsync(int userId);

    Task UnlockUserAsync(int userId);

    Task ChangeStatusAsync(int userId, bool isActive);

    Task DeleteUserAsync(int userId);
}