using KickZone.Contracts.DTOs.BrandDTOs;
using KickZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KickZone.Controllers.Admin;

[ApiController]
[Route("api/admin/brands")]
public class BrandsController : ControllerBase
{
    private readonly IBrandService _brandService;

    public BrandsController(IBrandService brandService)
    {
        _brandService = brandService;   
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _brandService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _brandService.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateBrandDto request)
    {
        await _brandService.CreateAsync(request);

        return Ok("Create brand successfully");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateBrandDto request)
    {
        await _brandService.UpdateAsync(id, request);

        return Ok("Update brand successfully");
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(
        int id,
        ChangeBrandStatusDto request)
    {
        await _brandService.ChangeStatusAsync(
            id,
            request.IsActive);

        return Ok("Change brand status successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _brandService.DeleteAsync(id);

        return Ok("Delete brand successfully");
    }
}