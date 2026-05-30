   
namespace SupplyChain.Repository.Procurement.Interfaces
{
    public interface IPurchaseOrderRepository
    {
        Task<List<PurchaseOrder>> GetAll();
        Task<PurchaseOrder?> GetById(int PoId);
        Task<PurchaseOrder> Create(PurchaseOrder po);
        Task<PurchaseOrder> Update(PurchaseOrder po);
        Task Delete(PurchaseOrder po);
        Task<List<PurchaseOrder>> Search(SearchByPurchaseDTO dto);
       Task<List<Supplier>> GetSuppliersForLookup();
    }
}