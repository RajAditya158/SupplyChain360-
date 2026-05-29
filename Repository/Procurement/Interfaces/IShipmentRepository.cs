namespace SupplyChain.Repository.Procurement.Interfaces
{
public interface IShipmentRepository
{
    Task<List<InboundShipment>> GetAll();
    Task<InboundShipment> GetById(int ShipmentId);
    Task<InboundShipment> Create(InboundShipment s);
    Task<InboundShipment> Update(InboundShipment s);
    Task<List<InboundShipment>> SearchShipment(SearchInboundShipmentDto dto);
}
}