namespace SupplyChain.Repository.Procurement.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetAll();
        Task<List<OrderItem>> GetByItemId(int itemId);
        Task<OrderItem?> GetById(int itemId);
        Task<OrderItem> Create(OrderItem item);
        Task Delete(int itemId);
        Task<List<OrderItem>>SearchOrder(SearchOrderDTO dto);
    }
}

