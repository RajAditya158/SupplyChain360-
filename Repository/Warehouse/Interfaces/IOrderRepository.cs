using Supplychain.Models.Warehouse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supplychain.Repository.Warehouse.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);
        Task<IEnumerable<Order>> GetCurrentOrdersAsync();
        Task<Order> GetByIdAsync(int orderId);
        Task UpdateOrderAsync(Order order);

        Task<IEnumerable<Order>> GetOrderHistoryAsync();
        
    }
}
