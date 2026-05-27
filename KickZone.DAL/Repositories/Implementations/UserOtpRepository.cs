using KickZone.DAL.Entities;
using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KickZone.Repositories.Implementations;

public class UserOtpRepository : IUserOtpRepository
{
    private readonly ShoeShopDbContext _context;

    public UserOtpRepository(ShoeShopDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(UserOTP otp)
    {
        await _context.UserOTPs.AddAsync(otp);

        await _context.SaveChangesAsync();
    }

public async Task<UserOTP?> GetValidOtpAsync(
    int userId,
    string otpCode)
{
    Console.WriteLine("LOCAL NOW: " + DateTime.Now);

    Console.WriteLine("UTC NOW: " + DateTime.UtcNow);

    var otp = await _context.UserOTPs
        .FirstOrDefaultAsync(x =>
            x.UserID == userId &&
            x.OTPCode == otpCode &&
            x.IsUsed == false
        );

    if (otp != null)
    {
        Console.WriteLine("EXPIRED AT: " + otp.ExpiredAt);
    }

    return otp;
}

    public async Task UpdateAsync(UserOTP otp)
    {
        _context.UserOTPs.Update(otp);

        await _context.SaveChangesAsync();
    }
}