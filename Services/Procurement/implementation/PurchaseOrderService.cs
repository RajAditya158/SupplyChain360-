
using Microsoft.AspNetCore.Http.HttpResults;
using SupplyChain.Repository.Procurement.Interfaces;
using SupplyChain.Services.Procurement.Interfaces;
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
        OrderDate = dto.OrderDate,
        ExpectedDeliveryDate = dto.ExpectedDeliveryDate,
        Status = "CREATED"
    };
    var createdPO = await _repo.Create(po);
    
    if (string.Equals(createdPO.Status, "CONFIRMED", StringComparison.OrdinalIgnoreCase))
    {
        var shipment = new InboundShipment
        {
            PoId = createdPO.PoId,
            SupplierId = createdPO.SupplierId,
            ExpectedDeliveryDate = createdPO.ExpectedDeliveryDate,
            Status = createdPO.Status,
            Carrier = "Blue Dot"
        };

        await _shipmentRepo.Create(shipment);
    }
    return createdPO;
}

    public async Task<PurchaseOrder> GetById(long id)
        => await _repo.GetById(id);

public async Task<PurchaseOrder> UpdateStatus(long id, string status)
{
    var po = await _repo.GetById(id);

    // ✅ FIX: check null
    if (po == null)
        throw new Exception("Purchase Order not found");

    po.Status = status;

    var updatedPO = await _repo.Update(po);

    if (string.Equals(status, "CONFIRMED", StringComparison.OrdinalIgnoreCase))
    {
        var shipment = new InboundShipment
        {
            PoId = updatedPO.PoId,
            SupplierId = updatedPO.SupplierId,
            ExpectedDeliveryDate = updatedPO.ExpectedDeliveryDate,
            Status = updatedPO.Status,
            Carrier = "Blue Dot"
        };

        await _shipmentRepo.Create(shipment);
    }

    return updatedPO;
}

public async Task Delete(long id)
{
    var po = await _repo.GetById(id);

    if (po == null)
        throw new Exception("Purchase Order not found");

    await _repo.Delete(po);
}

        public Task<PurchaseOrder> Update(long id, PurchaseOrderDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}