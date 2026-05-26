
namespace SupplyChain.Services.Procurement.Interfaces
{
    public interface IOrderItemService
    {
        Task<List<OrderItem>> GetByPO(long poId);
        Task<OrderItem> Add(long poId, OrderItemDTO dto);
        Task<OrderItem> Update(long itemId, OrderItemDTO dto);
        Task Delete(long itemId);
    }
}