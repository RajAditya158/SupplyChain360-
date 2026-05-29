using Microsoft.AspNetCore.Mvc;
using Supplychain.Dtos.Warehouse;
using Supplychain.Models.Warehouse;
using Supplychain.Services.Warehouse;

namespace Supplychain.Controllers.Warehouse
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService; // <-- Depend on interface

        public OrdersController(IOrderService orderService) // <-- Inject interface
        {
            _orderService = orderService;
        }

        // POST /api/orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto dto)
        {
            var order = new Order
            {
                CustomerName = dto.CustomerName,
                SKU = dto.SKU,
                ProductName = dto.ProductName,
                Quantity = dto.Quantity,
                Address = dto.Address,
                MobileNumber = dto.MobileNumber,
                ETA = dto.ETA
            };

            var createdOrder = await _orderService.CreateOrderAsync(order);
            return Ok(createdOrder);
        }

        // GET /api/orders/current
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentOrders()
        {
            var orders = await _orderService.GetCurrentOrdersAsync();
            return Ok(orders);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string newStatus)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateOrderStatusAsync(id, newStatus);
                return Ok(updatedOrder);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("history")]
        public async Task<IActionResult> GetOrderHistory()
        {
            var orders = await _orderService.GetOrderHistoryAsync();
            return Ok(orders);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            try
            {
                var cancelledOrder = await _orderService.CancelOrderAsync(id);
                return Ok(cancelledOrder);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
