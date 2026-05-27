using KickZone.Contracts.DTOs.Voucher;
using KickZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KickZone.Controllers.Admin;

[Route("api/admin/vouchers")]
[ApiController]
public class VouchersController : ControllerBase
{
    private readonly IVoucherService _service;

    public VouchersController(IVoucherService service)
    {
        _service = service;
    }

    // GET: api/admin/vouchers
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    // GET: api/admin/vouchers/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound(new { message = "Voucher not found" });

        return Ok(result);
    }

    // POST: api/admin/vouchers
    [HttpPost]  
    public async Task<IActionResult> Create([FromBody] CreateVoucherRequestDto request)
    {
        var createdId = await _service.CreateAsync(request);
        return CreatedAtAction(
            nameof(GetById),
            new { id = createdId },
            new { message = "Voucher created successfully", id = createdId }
        );
    }

    // PUT: api/admin/vouchers/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateVoucherRequestDto request)
    {
        var exists = await _service.ExistsAsync(id);
        if (!exists)
            return NotFound(new { message = "Voucher not found" });

        await _service.UpdateAsync(id, request);

        return Ok(new { message = "Voucher updated successfully" });
    }

    // PATCH: api/admin/vouchers/{id}/status
    [HttpPatch("{id}/status")] // bật /tắt voucher
    public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeVoucherStatusDto request)
    {
        var exists = await _service.ExistsAsync(id);
        if (!exists)
            return NotFound(new { message = "Voucher not found" });

        await _service.ChangeStatusAsync(id, request.IsActive);

        return Ok(new { message = "Status updated successfully" });
    }

    // DELETE: api/admin/vouchers/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var exists = await _service.ExistsAsync(id);
        if (!exists)
            return NotFound(new { message = "Voucher not found" });

        await _service.DeleteAsync(id);

        return Ok(new { message = "Voucher deleted successfully" });
    }
[HttpPost("apply")]
public async Task<IActionResult> ApplyVoucher([FromBody] ApplyVoucherRequestDto request)
{
    var result = await _service.ApplyVoucherAsync(request);
    return Ok(result);
}

}