using KickZone.Contracts.DTOs.ProductVariantDTOs;
using KickZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/admin/product-variants")]
[ApiController]
public class ProductVariantController : ControllerBase
{
    private readonly IProductVariantService _service;

    public ProductVariantController(IProductVariantService service)
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
        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductVariantRequestDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok("Created successfully");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductVariantRequestDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok("Updated successfully");
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(int id, ChangeVariantStatusDto dto)
    {
        await _service.ChangeStatusAsync(id, dto);
        return Ok("Status updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok("Deleted successfully");
    }
}