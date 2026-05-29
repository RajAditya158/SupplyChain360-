using Microsoft.AspNetCore.Mvc;
using Supplychain.Dtos.Warehouse;
using Supplychain.Models.Warehouse;
using Supplychain.Services.Warehouse;
using SupplyChain360.Enums.Warehouse;

namespace Supplychain.Controllers.Warehouse
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService; 

        public OrdersController(IOrderService orderService) 
        {
            _orderService = orderService;
        }

        // POST /api/orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto dto)
        {
            if (dto != null)
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
            return createdOrder != null ? Ok(createdOrder) : BadRequest(new { error = "Failed to create order" });
            }
           
           return BadRequest(new { error = "Invalid order data" });
        }

        // GET /api/orders/current
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentOrders()
        {
            var orders = await _orderService.GetCurrentOrdersAsync();
            return Ok(orders);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatus newStatus)
        {
            try
            {
                if (id > 0)
                {
                    var updatedOrder = await _orderService.UpdateOrderStatusAsync(id, newStatus);
                    return updatedOrder!=null ? Ok(updatedOrder) : BadRequest(new { error = "Failed to update order status" });
                }
                  return BadRequest(new { error = "Invalid order ID" });
                
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
                if (id > 0)
                {
                     var cancelledOrder = await _orderService.CancelOrderAsync(id);
                    return Ok(cancelledOrder);                    
                }
               
               return BadRequest(new { error = "Invalid order ID" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
