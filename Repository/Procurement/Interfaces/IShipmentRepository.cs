namespace SupplyChain.Repository.Procurement.Interfaces
{
public interface IShipmentRepository
{
    Task<List<InboundShipment>> GetAll();
    Task<InboundShipment> GetById(long id);
    Task<InboundShipment> Create(InboundShipment s);
    Task<InboundShipment> Update(InboundShipment s);
}
}