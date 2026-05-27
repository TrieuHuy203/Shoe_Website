using KickZone.Contracts.DTOs.UserDTOs;
using KickZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KickZone.Controllers.Admin;

[ApiController]
[Route("api/admin/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _userService.GetUsersAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequestDto request)
    {
        var result = await _userService.CreateUserAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserRequestDto request)
    {
        var result = await _userService.UpdateUserAsync(id, request);
        return Ok(result);
    }

    [HttpPatch("{id}/lock")]
    public async Task<IActionResult> LockUser(int id)
    {
        await _userService.LockUserAsync(id);
        return Ok("User locked");
    }

    [HttpPatch("{id}/unlock")]
    public async Task<IActionResult> UnlockUser(int id)
    {
        await _userService.UnlockUserAsync(id);
        return Ok("User unlocked");
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(int id, ChangeUserStatusRequestDto request)
    {
        await _userService.ChangeStatusAsync(id, request.IsActive);
        return Ok("Status updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok("User deleted");
    }
}