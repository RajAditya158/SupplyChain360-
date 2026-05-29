using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupplyChain.Repository.Procurement.Interfaces;
using SupplyChain.Services.Procurement.Interfaces;
using SupplyChain360.Enums.Procurement;

namespace SupplyChain.Services.Procurement.Implementation
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository _repo;

        public ShipmentService(IShipmentRepository repo)
        {
            _repo = repo;
        }

        //  Get all shipments
        public async Task<List<InboundShipment>> GetAll()
        {
            return await _repo.GetAll();
        }

        //  Get by ID
        public async Task<InboundShipment> GetById(int ShipmentId)
        {
            var shipment = await _repo.GetById(ShipmentId);

            if (shipment == null)
                throw new KeyNotFoundException("Shipment not found");

            return shipment; //  FIXED
        }
        //  Create shipment
        public async Task<InboundShipment> Create(ShipmentDTO dto)
        {
            var shipment = new InboundShipment
            {
                PoId = dto.PoId,
                Carrier = dto.Carrier,
                ExpectedDeliveryDate = dto.ExpectedDeliveryDate,
                Status = ShipmentStatus.Created
            };

            return await _repo.Create(shipment);
        }

        //  Update shipment
        public async Task<InboundShipment> Update(int ShipmentId, ShipmentDTO dto)
        {
            var s = await GetById(ShipmentId);

            s.Carrier = dto.Carrier;
            s.ExpectedDeliveryDate = dto.ExpectedDeliveryDate;
            if (dto.Status.HasValue)
                s.Status = dto.Status.Value;

            return await _repo.Update(s);
        }

        //  Update status
        public async Task<InboundShipment> UpdateStatus(int ShipmentId, ShipmentStatus status)
        {
            var s = await GetById(ShipmentId);
            s.Status = status;
            return await _repo.Update(s);
        }

        //  Update ETA
        public async Task<InboundShipment> UpdateETA(int ShipmentId, DateTime eta)
        {
            var s = await GetById(ShipmentId);
            s.ExpectedDeliveryDate = eta;
            return await _repo.Update(s);
        }

        //  Delete shipment
        public async Task Delete(int ShipmentId)
        {
            await ((dynamic)_repo).Delete(ShipmentId);
        }
        public async Task<List<InboundShipment>>Search(SearchInboundShipmentDto dto)
        {
            return await _repo.SearchShipment(dto);
        }
    }
}