namespace KickZone.DomainEntities.Entities;

using System.ComponentModel.DataAnnotations;

public class UserOTP
{

    [Key]
    public int OTPID { get; set; }

    public int UserID { get; set; }

    public string OTPCode { get; set; } = null!;

    public DateTime ExpiredAt { get; set; }

    public bool IsUsed { get; set; }

    public DateTime CreatedAt { get; set; }

    public User User { get; set; } = null!;
}