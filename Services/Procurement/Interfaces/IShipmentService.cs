
namespace SupplyChain.Services.Procurement.Interfaces
{
    public interface IShipmentService
    {
        Task<List<InboundShipment>> GetAll();
        Task<InboundShipment> GetById(long id);
        Task<List<InboundShipment>> GetByPO(long poId);
        Task<InboundShipment> Create(ShipmentDTO dto);
        Task<InboundShipment> Update(long id, ShipmentDTO dto);
        Task<InboundShipment> UpdateStatus(long id, string status);
        Task<InboundShipment> UpdateETA(long id, DateTime eta);
        Task Delete(long id);
    }
}