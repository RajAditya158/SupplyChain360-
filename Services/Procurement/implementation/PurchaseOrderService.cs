
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

    public async Task<PurchaseOrder> Create(PurchaseOrderDTO dto)
    {
    var po = new PurchaseOrder
    {
        SupplierId = dto.SupplierId,
        SupplierName = dto.SupplierName,
        OrderDate = dto.OrderDate,
        ExpectedDeliveryDate = dto.ExpectedDeliveryDate,
        Status = dto.Status == 0 ? PurchaseOrderStatus.Created : dto.Status
    };

    var createdPO = await _repo.Create(po);

    if (createdPO.Status == PurchaseOrderStatus.Confirmed)
    {
        var shipment = new InboundShipment
        {
            PoId = createdPO.PoId,
            SupplierId = createdPO.SupplierId,
            SupplierName = createdPO.SupplierName,
            ExpectedDeliveryDate = createdPO.ExpectedDeliveryDate,
            Status = ShipmentStatus.Created,
            Carrier = "Blue Dot"
        };

        await _shipmentRepo.Create(shipment);
    }

    return createdPO;
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
public async Task<PurchaseOrder> Update(int PoId, PurchaseOrderDTO dto)
{
    var po = await _repo.GetById(PoId);

    if (po == null)
        throw new Exception("Purchase Order not found");

    // Update ALL fields
    po.SupplierId = dto.SupplierId;
    po.SupplierName = dto.SupplierName;   
    po.OrderDate = dto.OrderDate;
    po.ExpectedDeliveryDate = dto.ExpectedDeliveryDate;
    po.Status = dto.Status;               

    var updatedPO = await _repo.Update(po);

    return updatedPO;
}

public async Task<List<PurchaseOrder>> Search(SearchByPurchaseDTO dto)
{
    return await _repo.Search(dto);
}

}
}