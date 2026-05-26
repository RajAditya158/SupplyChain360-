namespace SupplyChain.Services.Procurement.Interfaces
{
public interface IPurchaseOrderService
{
    Task<List<PurchaseOrder>> GetAll();
    Task<PurchaseOrder> Create(PurchaseOrderDTO dto);
    Task<PurchaseOrder> GetById(long id);
    Task<PurchaseOrder> Update(long id, PurchaseOrderDTO dto);
    Task<PurchaseOrder> UpdateStatus(long id, string status);
    Task Delete(long id);
}
}