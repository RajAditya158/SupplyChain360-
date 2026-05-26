using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupplyChain.Repository.Procurement.Interfaces;
using SupplyChain.Services.Procurement.Interfaces;

namespace SupplyChain.Services.Procurement.Implementation
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository _repo;

        public ShipmentService(IShipmentRepository repo)
        {
            _repo = repo;
        }

        // ✅ Get all shipments
        public async Task<List<InboundShipment>> GetAll()
        {
            return await _repo.GetAll();
        }

        // ✅ Get by ID
        public async Task<InboundShipment> GetById(long id)
        {
            var shipment = await _repo.GetById(id);

            if (shipment == null)
                throw new KeyNotFoundException("Shipment not found");

            return shipment; // ✅ FIXED
        }

        // ✅ Get by PO
        public async Task<List<InboundShipment>> GetByPO(long poId)
        {
            var shipments = await _repo.GetAll();
            return shipments.Where(s => s.PoId == poId).ToList();
        }

        // ✅ Create shipment
        public async Task<InboundShipment> Create(ShipmentDTO dto)
        {
            var shipment = new InboundShipment
            {
                PoId = dto.PoId,
                Carrier = dto.Carrier,
                ExpectedDeliveryDate = dto.ExpectedDeliveryDate,
                Status = "CREATED"
            };

            return await _repo.Create(shipment);
        }

        // ✅ Update shipment
        public async Task<InboundShipment> Update(long id, ShipmentDTO dto)
        {
            var s = await GetById(id);

            s.Carrier = dto.Carrier;
            s.ExpectedDeliveryDate = dto.ExpectedDeliveryDate;
            s.Status = dto.Status;

            return await _repo.Update(s);
        }

        // ✅ Update status
        public async Task<InboundShipment> UpdateStatus(long id, string status)
        {
            var s = await GetById(id);
            s.Status = status;
            return await _repo.Update(s);
        }

        // ✅ Update ETA
        public async Task<InboundShipment> UpdateETA(long id, DateTime eta)
        {
            var s = await GetById(id);
            s.ExpectedDeliveryDate = eta;
            return await _repo.Update(s);
        }

        // ✅ Delete shipment
        public async Task Delete(long id)
        {
            await ((dynamic)_repo).Delete(id);
        }
    }
}