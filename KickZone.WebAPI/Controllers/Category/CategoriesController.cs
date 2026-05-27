
using KickZone.Contracts.DTOs.Category;
using KickZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KickZone.Controllers;

[Route("api/admin/categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
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
    public async Task<IActionResult> Create(CreateCategoryRequestDto request)
    {
        await _service.CreateAsync(request);
        return Ok("Created successfully");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryRequestDto request)
    {
        await _service.UpdateAsync(id, request);
        return Ok("Updated successfully");
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(int id)
    {
        await _service.ChangeStatusAsync(id);
        return Ok("Status updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok("Deleted successfully");
    }
}