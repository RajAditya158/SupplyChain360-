using Microsoft.AspNetCore.Mvc;
using SupplyChain.Services.Procurement.Interfaces;

[ApiController]
[Route("api/v1/order-items")]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService _service;

    public OrderItemController(IOrderItemService service)
    { 
        _service = service;
    }

    [HttpGet("/api/v1/order-items")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("{ItemId}")]
    public async Task<IActionResult> Get(int ItemId)
        => Ok(await _service.GetByItemId(ItemId));

    [HttpPost]
    public async Task<IActionResult> Add(int poId, OrderItemDTO dto)
        => Ok(await _service.Add(poId, dto));
        
    [HttpDelete("{itemId}")]
   public async Task<IActionResult> Delete(int itemId)
    {
        await _service.Delete(itemId);
        return Ok("Item deleted successfully");
    }
        [HttpGet("search-order")]
        public async Task<IActionResult> SearchOrder(
            [FromQuery] int? poId,
            [FromQuery] string? itemName)
        {
            var dto = new SearchOrderDTO
            {
                PoId = poId ?? 0,
                ItemName = itemName
            };

            var result = await _service.SearchOrder(dto);
            return Ok(result);
        }
}