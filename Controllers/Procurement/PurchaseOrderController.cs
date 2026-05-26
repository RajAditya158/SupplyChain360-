using Microsoft.AspNetCore.Mvc;
using SupplyChain.Services.Procurement.Interfaces;

[ApiController]
[Route("api/v1/purchase-orders")]
public class PurchaseOrderController : ControllerBase
{
    private readonly IPurchaseOrderService _service;

    public PurchaseOrderController(IPurchaseOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAll());

    [HttpPost]
    public async Task<IActionResult> Create(PurchaseOrderDTO dto)
        => Ok(await _service.Create(dto));

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(long id, StatusUpdateDTO dto)
        => Ok(await _service.UpdateStatus(id, dto.Status));
        
[HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.Delete(id);
        return Ok("Purchase Order deleted successfully");
    }

}