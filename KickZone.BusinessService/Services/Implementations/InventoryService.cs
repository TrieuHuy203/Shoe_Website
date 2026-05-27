using KickZone.Contracts.DTOs.InventoryDTOs;
using KickZone.Repositories.Interfaces;
using KickZone.DomainEntities.Entities;
using KickZone.Services.Interfaces;

namespace KickZone.Services.Implementations;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _repo;

    public InventoryService(IInventoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<InventoryDto>> GetAllAsync()
    {
        var data = await _repo.GetAllAsync();

        return data.Select(x => new InventoryDto
        {
            InventoryId = x.InventoryId,
            ProductVariantId = x.ProductVariantId,
            Quantity = x.Quantity,
            Status = GetStatus(x.Quantity),
            UpdatedAt = x.UpdatedAt
        }).ToList();
    }

    public async Task<InventoryDto> GetByIdAsync(int id)
    {
        var x = await _repo.GetByIdAsync(id);
        if (x == null) return null!;

        return new InventoryDto
        {
            InventoryId = x.InventoryId,
            ProductVariantId = x.ProductVariantId,
            Quantity = x.Quantity,
            Status = GetStatus(x.Quantity),
            UpdatedAt = x.UpdatedAt
        };
    }

    public async Task<bool> UpdateStockAsync(int id, UpdateInventoryDto dto)
    {
        if (dto.Quantity < 0) return false;

        var inventory = await _repo.GetByIdAsync(id);
        if (inventory == null) return false;

        inventory.Quantity = dto.Quantity;
        inventory.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(inventory);
        await _repo.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ImportStockAsync(StockTransactionDto dto)
    {
        if (dto.Quantity <= 0) return false;

        var inventory = await _repo.GetByVariantIdAsync(dto.ProductVariantId);
        if (inventory == null) return false;

        inventory.Quantity += dto.Quantity;
        inventory.UpdatedAt = DateTime.UtcNow;

        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExportStockAsync(StockTransactionDto dto)
    {
        if (dto.Quantity <= 0) return false;

        var inventory = await _repo.GetByVariantIdAsync(dto.ProductVariantId);
        if (inventory == null) return false;

        if (inventory.Quantity < dto.Quantity)
            throw new Exception("Insufficient stock");

        inventory.Quantity -= dto.Quantity;
        inventory.UpdatedAt = DateTime.UtcNow;

        await _repo.SaveChangesAsync();
        return true;
    }

    private string GetStatus(int quantity)
    {
        if (quantity <= 0) return "OutOfStock";
        if (quantity < 10) return "LowStock";
        return "InStock";
    }
}