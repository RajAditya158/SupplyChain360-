   
namespace SupplyChain.Repository.Procurement.Interfaces
{
    public interface IPurchaseOrderRepository
    {
        Task<List<PurchaseOrder>> GetAll();
        Task<PurchaseOrder?> GetById(long id);
        Task<PurchaseOrder> Create(PurchaseOrder po);
        Task<PurchaseOrder> Update(PurchaseOrder po);
        Task Delete(PurchaseOrder po);
    }
}