using KickZone.DAL.Entities;
using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class VoucherRepository : IVoucherRepository
{
    private readonly ShoeShopDbContext _context;

    public VoucherRepository(ShoeShopDbContext context)
    {
        _context = context;
    }

    public async Task<List<Voucher>> GetAllAsync()
        => await _context.Vouchers
            .Where(x => !x.IsDeleted)
            .ToListAsync();

    public async Task<Voucher?> GetByIdAsync(int id)
        => await _context.Vouchers.FirstOrDefaultAsync(x => x.VoucherId == id && !x.IsDeleted);

    public async Task<Voucher?> GetByCodeAsync(string code)
        => await _context.Vouchers.FirstOrDefaultAsync(x => x.Code == code && !x.IsDeleted);

    public async Task AddAsync(Voucher voucher)
        => await _context.Vouchers.AddAsync(voucher);

    public Task UpdateAsync(Voucher voucher)
    {
        _context.Vouchers.Update(voucher);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Voucher voucher)
    {
        voucher.IsDeleted = true;
        return Task.CompletedTask;
    }
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Vouchers.AnyAsync(x => x.VoucherId == id && !x.IsDeleted);
    }

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}