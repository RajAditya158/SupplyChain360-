using SupplyChain360.Enums.Procurement;

namespace SupplyChain.Services.Procurement.Interfaces
{
public interface IPurchaseOrderService
{
    Task<List<PurchaseOrder>> GetAll();
    Task<PurchaseOrder> Create(PurchaseOrderDTO dto);
    Task<PurchaseOrder> GetById(int PoId);
    Task<PurchaseOrder> Update(int PoId, PurchaseOrderDTO dto);
    Task<PurchaseOrder> UpdateStatus(int PoId, PurchaseOrderStatus status);
    Task Delete(int PoId);
    Task<List<PurchaseOrder>> Search(SearchByPurchaseDTO dto);
    }
}