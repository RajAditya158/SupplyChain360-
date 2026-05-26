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

        public async Task<List<OrderItem>> GetByPO(long poId)
        {
            return await _repo.GetByPO(poId);
        }

        public async Task<OrderItem> Add(long poId, OrderItemDTO dto)
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

        public async Task<OrderItem> Update(long id, OrderItemDTO dto)
        {
            var item = await _repo.GetById(id);

            if (item == null)
                throw new KeyNotFoundException("Order Item not found");

            item.ItemName = dto.ItemName;
            item.Quantity = dto.Quantity;
            item.Price = dto.Price;

            return await _repo.Create(item);
        }

        public async Task Delete(long id)
        {
            await _repo.Delete(id);
        }
    }
}