using Microsoft.AspNetCore.Mvc;
using SupplyChain.Services.Procurement.Interfaces;
using SupplyChain360.Enums.Procurement;

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

    [HttpPatch("{poId}/status")]
    public async Task<IActionResult> UpdateStatus(int poId, [FromBody] PurchaseOrderStatus status)
        => Ok(await _service.UpdateStatus(poId, status));
        
[HttpDelete("{PoId}")]
    public async Task<IActionResult> Delete(int PoId)
    {
        if (PoId > 0)
        {
            await _service.Delete(PoId);
            return Ok("Purchase Order deleted successfully");
        }
       

        return BadRequest("Invalid ID");
            
    }

[HttpGet("search")]
public async Task<IActionResult> Search(
    [FromQuery] string? supplierName,
    [FromQuery] DateTime? fromDate,
    [FromQuery] DateTime? toDate,
    [FromQuery] PurchaseOrderStatus? status)
{
    var dto = new SearchByPurchaseDTO
    {
        SupplierName = supplierName,
        FromDate = fromDate,
        ToDate = toDate,
        Status = status
    };

    var result = await _service.Search(dto);
    return Ok(result);
}

[HttpPut("{PoId}")]
public async Task<IActionResult> Update(int PoId, PurchaseOrderDTO po)
{
    var updatedPO = await _service.Update(PoId, po);
    return Ok(updatedPO);
}
}
