using Microsoft.EntityFrameworkCore;
using Supplychain.Data;
using SupplyChain.Repository.Procurement.Interfaces;


namespace SupplyChain.Repository.Procurement.Implementation
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly SupplyChainContext _context;

        public OrderItemRepository(SupplyChainContext context)
        {
            _context = context;
        }

        // ✅ Get all items for a specific Purchase Order
        public async Task<List<OrderItem>> GetByPO(long poId)
        {
            return await _context.OrderItems
                                 .Where(x => x.PoId == poId)
                                 .ToListAsync();
        }

        // ✅ Get single item by ID
        public async Task<OrderItem?> GetById(long id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        // ✅ Create new order item
        public async Task<OrderItem> Create(OrderItem item)
        {
            await _context.OrderItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        // ✅ Update existing item
        public async Task<OrderItem> Update(OrderItem item)
        {
            _context.OrderItems.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        // ✅ Delete item
        public async Task Delete(long id)
        {
            var item = await _context.OrderItems.FindAsync(id);

            if (item != null)
            {
                _context.OrderItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}