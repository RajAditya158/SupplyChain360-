using Supplychain.Data;
using Supplychain.Models.Warehouse;
using Supplychain.Repository.Warehouse.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplychain.Repository.Warehouse.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SupplyChainContext _context;

        public OrderRepository(SupplyChainContext context)
        {
            _context = context;
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetByIdAsync(int orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<IEnumerable<Order>> GetCurrentOrdersAsync()
        {
            return await _context.Orders
                .Where(o => o.Status == "Pending" || o.Status == "Confirmed")
                .ToListAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetOrderHistoryAsync()
        {
            return await _context.Orders
                .Where(o => o.Status == "Shipped" 
                        || o.Status == "Delivered" 
                        || o.Status == "Cancelled")
                .ToListAsync();
        }
    }
}