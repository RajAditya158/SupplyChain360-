
namespace SupplyChain.Services.Procurement.Interfaces
{
    public interface IOrderItemService
    {
        Task<List<OrderItem>> GetByItemId(int ItemId );
        Task<OrderItem> Add(int poId, OrderItemDTO dto);
        Task<OrderItem> Update(int ItemId, OrderItemDTO dto);
        Task Delete(int ItemId);
        Task<List<OrderItem>> GetAll();
        Task<List<OrderItem>>SearchOrder(SearchOrderDTO dto);
    }
}