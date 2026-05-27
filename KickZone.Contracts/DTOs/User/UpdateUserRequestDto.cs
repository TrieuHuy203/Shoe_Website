namespace KickZone.Contracts.DTOs.UserDTOs;

public class UpdateUserRequestDto
{
    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public bool? IsActive { get; set; }
}