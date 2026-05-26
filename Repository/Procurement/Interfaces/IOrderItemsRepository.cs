namespace SupplyChain.Repository.Procurement.Interfaces
{
public interface IOrderItemRepository
{
    Task<List<OrderItem>> GetByPO(long poId);
    Task<OrderItem> Create(OrderItem item);
    Task Delete(long id);
    Task<OrderItem> GetById(long id);
}
}
