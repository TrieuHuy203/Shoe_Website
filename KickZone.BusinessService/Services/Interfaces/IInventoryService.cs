using KickZone.Contracts.DTOs.InventoryDTOs;

namespace KickZone.Services.Interfaces;

public interface IInventoryService
{
    Task<List<InventoryDto>> GetAllAsync();
    Task<InventoryDto> GetByIdAsync(int id);

    Task<bool> UpdateStockAsync(int id, UpdateInventoryDto dto);
    Task<bool> ImportStockAsync(StockTransactionDto dto);
    Task<bool> ExportStockAsync(StockTransactionDto dto);
}