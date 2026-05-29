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
            return await _context.OrderItems.ToListAsync();
        }

        //  Get all items for a specific Purchase Order
        public async Task<List<OrderItem>> GetByItemId(int ItemId)
        {
            return await _context.OrderItems
                                 .Where(x => x.ItemId == ItemId)
                                 .ToListAsync();
        }

        //  Get single item by ID
        public async Task<OrderItem?> GetById(int ItemId)
        {
            return await _context.OrderItems.FindAsync(ItemId);
        }

        //  Create new order item
        public async Task<OrderItem> Create(OrderItem item)
        {
            await _context.OrderItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        //  Update existing item
        public async Task<OrderItem> Update(OrderItem item)
        {
            _context.OrderItems.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        //  Delete item
        public async Task Delete(int ItemId)
        {
            var item = await _context.OrderItems.FindAsync(ItemId);

            if (item != null)
            {
                _context.OrderItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
      public async Task<List<OrderItem>> SearchOrder(SearchOrderDTO dto)
{
    var query = _context.OrderItems.AsQueryable();

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