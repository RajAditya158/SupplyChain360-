using Microsoft.EntityFrameworkCore;
using Supplychain.Data;
using SupplyChain.Repository.Procurement.Interfaces;

namespace SupplyChain.Repository.Procurement.Implementation
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly SupplyChainContext _context;

        // ✅ Constructor Injection
        public ShipmentRepository(SupplyChainContext context)
        {
            _context = context;
        }

        // ✅ Get all inbound shipments
        public async Task<List<InboundShipment>> GetAll()
        {
            return await _context.Shipments.ToListAsync();
        }

        // ✅ Get shipment by ID
        public async Task<InboundShipment?> GetById(long id)
        {
            return await _context.Shipments
                                 .FirstOrDefaultAsync(s => s.ShipmentId == id);
        }

        // ✅ Get shipments by Purchase Order ID
        public async Task<List<InboundShipment>> GetByPO(long poId)
        {
            return await _context.Shipments
                                 .Where(s => s.PoId == poId)
                                 .ToListAsync();
        }

        // ✅ Create new shipment
        public async Task<InboundShipment> Create(InboundShipment shipment)
        {
            await _context.Shipments.AddAsync(shipment);
            await _context.SaveChangesAsync();
            return shipment;
        }

        // ✅ Update existing shipment
        public async Task<InboundShipment> Update(InboundShipment shipment)
        {
            var existing = await _context.Shipments
                                         .FirstOrDefaultAsync(s => s.ShipmentId == shipment.ShipmentId);

            if (existing == null)
            {
                throw new KeyNotFoundException("Shipment not found");
            }

            existing.Carrier = shipment.Carrier;
            existing.ExpectedDeliveryDate = shipment.ExpectedDeliveryDate;
            existing.Status = shipment.Status;
            existing.PoId = shipment.PoId;

            _context.Shipments.Update(existing);
            await _context.SaveChangesAsync();

            return existing;
        }

        // ✅ Delete shipment
        public async Task Delete(long id)
        {
            var shipment = await _context.Shipments
                                         .FirstOrDefaultAsync(s => s.ShipmentId == id);

            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
                await _context.SaveChangesAsync();
            }
        }
    }
}