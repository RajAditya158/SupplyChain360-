using SupplyChain360.Enums.Procurement;

namespace SupplyChain.Services.Procurement.Interfaces
{
    public interface IShipmentService
    {
        Task<List<InboundShipment>> GetAll();
        Task<InboundShipment> GetById(int ShipmentId);
        Task<InboundShipment> Create(ShipmentDTO dto);
        Task<InboundShipment> Update(int ShipmentId, ShipmentDTO dto);
        Task<InboundShipment> UpdateStatus(int ShipmentId, ShipmentStatus status);
        Task<InboundShipment> UpdateETA(int ShipmentId, DateTime eta);
        Task Delete(int ShipmentId);
        Task<List<InboundShipment>>Search(SearchInboundShipmentDto dto);
    }
}