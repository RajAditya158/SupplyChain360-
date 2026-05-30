
using Microsoft.AspNetCore.Http.HttpResults;
using SupplyChain.Repository.Procurement.Interfaces;
using SupplyChain.Services.Procurement.Interfaces;
using SupplyChain360.Enums.Procurement;

namespace SupplyChain.Services.Procurement.Implementation
{
public class PurchaseOrderService : IPurchaseOrderService
{
    private readonly IPurchaseOrderRepository _repo;
    private readonly IShipmentRepository _shipmentRepo;
    public PurchaseOrderService(IPurchaseOrderRepository repo, IShipmentRepository shipmentRepo)
    {
        _repo = repo;
        _shipmentRepo = shipmentRepo;
    }

    public async Task<List<PurchaseOrder>> GetAll()
        => await _repo.GetAll();

   public async Task<PurchaseOrder?> Create(PurchaseOrderDTO dto)
{
    // 1. Ask the Repo for the list of Suppliers
    var suppliers = await _repo.GetSuppliersForLookup();

    // 2. Find the supplier that matches the name you picked in the dropdown
    var dbSupplier = suppliers.FirstOrDefault(s => 
        s.Name.Equals(dto.SupplierName.ToString(), StringComparison.OrdinalIgnoreCase));

    if (dbSupplier == null) return null;

    // 3. The ID and Name are now EXTRACTED automatically from the database record
    var po = new PurchaseOrder
    {
        SupplierId = dbSupplier.SupplierId, // Extracted!
        SupplierName = dbSupplier.Name,    // Extracted!
        OrderDate = dto.OrderDate,
        ExpectedDeliveryDate = dto.ExpectedDeliveryDate,
        Status = dto.Status == 0 ? PurchaseOrderStatus.Created : dto.Status
    };

    return await _repo.Create(po);
}

    public async Task<PurchaseOrder> GetById(int PoId)
        => await _repo.GetById(PoId);

public async Task<PurchaseOrder> UpdateStatus(int PoId, PurchaseOrderStatus status)
{
    var po = await _repo.GetById(PoId);

    //  FIX: check null
    if (po == null)
        throw new Exception("Purchase Order not found");

    po.Status = status;

    var updatedPO = await _repo.Update(po);

    if (status == PurchaseOrderStatus.Confirmed)
    {
        var shipment = new InboundShipment
        {
            PoId = updatedPO.PoId,
            SupplierId = updatedPO.SupplierId,
            SupplierName = updatedPO.SupplierName,
            ExpectedDeliveryDate = updatedPO.ExpectedDeliveryDate,
            Status = ShipmentStatus.Created,
            Carrier = "Blue Dot"
        };

        await _shipmentRepo.Create(shipment);
    }

    return updatedPO;
}

public async Task Delete(int PoId)
{
    var po = await _repo.GetById(PoId);

    if (po == null)
        throw new Exception("Purchase Order not found");

    await _repo.Delete(po);
}
public async Task<PurchaseOrder?> Update(int PoId, PurchaseOrderDTO dto)
{
    // 1. Find the existing Purchase Order
    var po = await _repo.GetById(PoId);
    if (po == null) return null;

    // 2. FETCH Suppliers and find the one matching the NAME selected in the dropdown
    var suppliers = await _repo.GetSuppliersForLookup();
    
    // We convert the Enum selection to string to find the match in the DB
    var dbSupplier = suppliers.FirstOrDefault(s => 
        s.Name.Equals(dto.SupplierName.ToString(), StringComparison.OrdinalIgnoreCase));
    
    // If name doesn't exist in the Supplier table, return null
    if (dbSupplier == null) return null;

    // 3. AUTOMATIC SYNC: Update ID and Name from the Database record
    po.SupplierId = dbSupplier.SupplierId; // ID extracted automatically
    po.SupplierName = dbSupplier.Name;     // Name extracted automatically
    
    po.OrderDate = dto.OrderDate;
    po.ExpectedDeliveryDate = dto.ExpectedDeliveryDate;
    po.Status = dto.Status;

    return await _repo.Update(po);
}

public async Task<List<PurchaseOrder>> Search(SearchByPurchaseDTO dto)
{
    return await _repo.Search(dto);
}

}
}