using SupplyChain.Repository.Procurement.Interfaces;
using SupplyChain.Services.Procurement.Interfaces;
namespace SupplyChain.Services.Procurement.Implementation
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _repo;

        public OrderItemService(IOrderItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<OrderItem>> GetByItemId(int ItemId)
        {
            return await _repo.GetByItemId(ItemId);
        }   
        public async Task<OrderItem> Add(int poId, OrderItemDTO dto)
        {
            var item = new OrderItem
            {
                PoId = poId,
                ItemName = dto.ItemName,
                Quantity = dto.Quantity,
                Price = dto.Price
            };

            return await _repo.Create(item);
        }

        public async Task<OrderItem> Update(int ItemId, OrderItemDTO dto)
        {
            var item = await _repo.GetById(ItemId);

            if (item == null)
                throw new KeyNotFoundException("Order Item not found");

            item.ItemName = dto.ItemName;
            item.Quantity = dto.Quantity;
            item.Price = dto.Price;

            return await _repo.Create(item);
        }

        public async Task Delete(int ItemId)
        {
            await _repo.Delete(ItemId);
        }

        
        public async Task<List<OrderItem>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<List<OrderItem>> SearchOrder(SearchOrderDTO dto)
        {
            return await _repo.SearchOrder(dto);
        }

    }
}