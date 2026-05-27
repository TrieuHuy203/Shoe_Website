using KickZone.Contracts.DTOs.ProductDTOs;
using KickZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KickZone.Controllers.Admin;

[ApiController]
[Route("api/admin/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _productService.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        await _productService.CreateAsync(dto);

        return Ok(new
        {
            message = "Product created successfully"
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductDto dto)
    {
        await _productService.UpdateAsync(id, dto);

        return Ok(new
        {
            message = "Product updated successfully"
        });
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(
        int id,
        ChangeProductStatusDto dto)
    {
        await _productService.ChangeStatusAsync(id, dto);

        return Ok(new
        {
            message = "Product status updated successfully"
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);

        return Ok(new
        {
            message = "Product deleted successfully"
        });
    }
}