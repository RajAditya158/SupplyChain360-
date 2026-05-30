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
        
        public async Task<List<OrderItem>> GetAll()
        {
            return await _context.Set<OrderItem>().ToListAsync();
        }

        //  Get all items for a specific Purchase Order
        public async Task<List<OrderItem>> GetByItemId(int ItemId)
        {
            return await _context.Set<OrderItem>()
                                 .Where(x => x.ItemId == ItemId)
                                 .ToListAsync();
        }

        //  Get single item by ID
        public async Task<OrderItem?> GetById(int ItemId)
        {
            return await _context.Set<OrderItem>().FindAsync(ItemId);
        }

        //  Create new order item
        public async Task<OrderItem> Create(OrderItem item)
        {
            await _context.Set<OrderItem>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        //  Update existing item
        public async Task<OrderItem> Update(OrderItem item)
        {
            _context.Set<OrderItem>().Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        //  Delete item
        public async Task Delete(int ItemId)
        {
            var item = await _context.Set<OrderItem>().FindAsync(ItemId);

            if (item != null)
            {
                _context.Set<OrderItem>().Remove(item);
                await _context.SaveChangesAsync();
            }
        }
      public async Task<List<OrderItem>> SearchOrder(SearchOrderDTO dto)
{
    var query = _context.Set<OrderItem>().AsQueryable();

    // Filter by PO
    if (dto.PoId > 0)
    {
        query = query.Where(o => o.PoId == dto.PoId);
    }

    // Optional filter by ItemName
    if (!string.IsNullOrEmpty(dto.ItemName))
    {
        query = query.Where(o =>
            o.ItemName.ToLower().Contains(dto.ItemName.ToLower()));
    }

    return await query
        .Select(o => new OrderItem
        {
            ItemId = o.ItemId,
            PoId = o.PoId,
            ItemName = o.ItemName,
            Quantity = o.Quantity,
            Price = o.Price
        })
        .ToListAsync();
}

    }
}