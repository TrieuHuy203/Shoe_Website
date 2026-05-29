using KickZone.Contracts.DTOs.InventoryDTOs;
using KickZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KickZone.API.Controllers.Admin;

[Route("api/admin/inventories")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _service;

    public InventoryController(IInventoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound("Inventory not found");
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStock(int id, UpdateInventoryDto dto)
    {
        var result = await _service.UpdateStockAsync(id, dto);
        if (!result) return BadRequest("Validation failed");

        return Ok("Updated successfully");
    }

    [HttpPost("import")]
    public async Task<IActionResult> Import([FromBody] StockTransactionDto dto)
    {
        var result = await _service.ImportStockAsync(dto);
        if (!result) return BadRequest("Invalid inventory information");

        return Ok("Stock imported");
    }

    [HttpPost("export")]
public async Task<IActionResult> Export([FromBody] StockTransactionDto dto)
    {
        var result = await _service.ExportStockAsync(dto);
        if (!result) return BadRequest("Invalid inventory information");

        return Ok("Stock exported");
    }
}