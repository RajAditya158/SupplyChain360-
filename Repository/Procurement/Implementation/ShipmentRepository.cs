using Microsoft.EntityFrameworkCore;
using Supplychain.Data;
using SupplyChain.Repository.Procurement.Interfaces;
using SupplyChain360.Enums.Procurement;

namespace SupplyChain.Repository.Procurement.Implementation
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly SupplyChainContext _context;

        //  Constructor Injection
        public ShipmentRepository(SupplyChainContext context)
        {
            _context = context;
        }

        //  Get all inbound shipments
        public async Task<List<InboundShipment>> GetAll()
        {
            return await _context.Shipments.ToListAsync();
        }

        //  Get shipment by ID
        public async Task<InboundShipment?> GetById(int ShipmentId)
        {
            return await _context.Shipments
                                 .FirstOrDefaultAsync(s => s.ShipmentId == ShipmentId);
        }
        //  Create new shipment
        public async Task<InboundShipment> Create(InboundShipment shipment)
        {
            await _context.Shipments.AddAsync(shipment);
            await _context.SaveChangesAsync();
            return shipment;
        }

        //  Update existing shipment
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

        //  Delete shipment
        public async Task Delete(int ShipmentId)
        {
            var shipment = await _context.Shipments
                                         .FirstOrDefaultAsync(s => s.ShipmentId == ShipmentId);

            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
                await _context.SaveChangesAsync();
            }
        }
        //  Search shipments based on criteria
       public async Task<List<InboundShipment>> SearchShipment(SearchInboundShipmentDto dto)
        {
            var query = _context.Shipments.AsQueryable();

            // Filter by PO
            if (dto.PoId > 0)
            {
                query = query.Where(s => s.PoId == dto.PoId);
            }

            // Filter by Status
            if (dto.Status.HasValue)
            {
                query = query.Where(s => s.Status == dto.Status.Value);
            }

            // check for null before comparing supplier name
            if (!string.IsNullOrEmpty(dto.SupplierName))
            {
                query = query.Where(s =>
                    s.SupplierName != null &&
                    s.SupplierName.ToLower() == dto.SupplierName.ToLower());
            }

            return await query.ToListAsync();
        }


    }
}