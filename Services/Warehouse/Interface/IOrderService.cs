using Supplychain.Models.Warehouse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supplychain.Services.Warehouse
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetCurrentOrdersAsync();
        Task<Order> UpdateOrderStatusAsync(int orderId, string newStatus);
        Task<IEnumerable<Order>> GetOrderHistoryAsync();

        Task<Order> CancelOrderAsync(int orderId);
    }
}
