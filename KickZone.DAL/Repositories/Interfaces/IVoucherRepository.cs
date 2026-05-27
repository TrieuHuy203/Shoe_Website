using KickZone.DomainEntities.Entities;

namespace KickZone.Repositories.Interfaces;

public interface IVoucherRepository
{
    Task<List<Voucher>> GetAllAsync();
    Task<Voucher?> GetByIdAsync(int id);
    Task<Voucher?> GetByCodeAsync(string code);

    Task AddAsync(Voucher voucher);
    Task UpdateAsync(Voucher voucher);
    Task DeleteAsync(Voucher voucher);
Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}