using Microsoft.AspNetCore.Mvc;
using SupplyChain.Services.Procurement.Interfaces;

[ApiController]
[Route("api/v1/purchase-orders/{poId}/items")]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService _service;

    public OrderItemController(IOrderItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(long poId)
        => Ok(await _service.GetByPO(poId));

    [HttpPost]
    public async Task<IActionResult> Add(long poId, OrderItemDTO dto)
        => Ok(await _service.Add(poId, dto));
        
[HttpDelete("{itemId}")]
    public async Task<IActionResult> Delete(long itemId)
    {
        await _service.Delete(itemId);
        return Ok("Item deleted successfully");
    }


}