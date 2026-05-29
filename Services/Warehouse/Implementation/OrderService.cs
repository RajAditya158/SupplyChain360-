using Supplychain.Models.Warehouse;
using Supplychain.Repository.Warehouse.Implementations;
using Supplychain.Repository.Warehouse.Interfaces;
using SupplyChain360.Enums.Warehouse;

namespace Supplychain.Services.Warehouse
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public OrderService(IOrderRepository orderRepository, IInventoryRepository inventoryRepository)
        {
            _orderRepository = orderRepository;
            _inventoryRepository = inventoryRepository;
        }

        /// <summary>
        /// Creates a new order, setting system-generated fields.
        /// </summary>
        public async Task<Order> CreateOrderAsync(Order order)
        {
            // System-generated fields
         
            // return await _orderRepository.AddOrderAsync(order);
            var inventory = await _inventoryRepository.GetBySkuAsync(order.SKU);
            // Check inventory before saving order
            if (inventory == null)
            {
                throw new InvalidOperationException($"SKU {order.SKU} not found in inventory.");
            }

            if (inventory.QuantityOnHand < order.Quantity)
            {
                throw new InvalidOperationException($"Insufficient stock for SKU {order.SKU}. Available: {inventory.QuantityOnHand}, Requested: {order.Quantity}");
            }
            inventory.QuantityOnHand -= order.Quantity;
            inventory.LastUpdated = DateTime.UtcNow;
            await _inventoryRepository.UpdateInventoryAsync(inventory);
            order.Status = OrderStatus.Pending;
            order.ShipmentId = Guid.NewGuid().ToString();
            order.OrderDate = DateTime.UtcNow;
            var createdOrder = await _orderRepository.AddOrderAsync(order);

            return createdOrder;
        }

        /// <summary>
        /// Retrieves all current orders (Pending or Confirmed).
        /// </summary>
        public async Task<IEnumerable<Order>> GetCurrentOrdersAsync()
        {
            return await _orderRepository.GetCurrentOrdersAsync();
        }


        public async Task<Order> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new InvalidOperationException($"Order {orderId} not found.");

            order.Status = newStatus;
            await _orderRepository.UpdateOrderAsync(order);

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrderHistoryAsync()
        {
            return await _orderRepository.GetOrderHistoryAsync();
        }
        public async Task<Order> CancelOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new InvalidOperationException($"Order {orderId} not found.");

            if (order.Status == OrderStatus.Cancelled)
                throw new InvalidOperationException($"Order {orderId} is already cancelled.");

            // Restore stock
            var inventory = await _inventoryRepository.GetBySkuAsync(order.SKU);
            if (inventory != null)
            {
                inventory.QuantityOnHand += order.Quantity;
                inventory.LastUpdated = DateTime.UtcNow;
                await _inventoryRepository.UpdateInventoryAsync(inventory);
            }

            // Update order status
            order.Status = OrderStatus.Cancelled;
            await _orderRepository.UpdateOrderAsync(order);

            return order;
        }

        
    }
}
