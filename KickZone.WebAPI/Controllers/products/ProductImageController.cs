using KickZone.Contracts.DTOs.ProductImage;
using KickZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KickZone.Controllers.Admin;


[ApiController]
[Route("api/admin/product-images")]
public class ProductImageController : ControllerBase
{
    private readonly IProductImageService _service;

    public ProductImageController(IProductImageService service)
    {
        _service = service;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] CreateProductImageRequestDto request)
    {
        var result = await _service.UploadAsync(request);
        return Ok(result);
    }

    [HttpPatch("{id}/thumbnail")] // thay đổi ảnh đại diện nếu muốn
    
    public async Task<IActionResult> SetMain(int id)
    {
        await _service.SetMainImageAsync(id);
        return Ok("Set main success");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok("Deleted");
    }
}