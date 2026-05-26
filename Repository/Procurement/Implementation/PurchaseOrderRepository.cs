using Supplychain.Data;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Repository.Procurement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SupplyChain.Repository.Procurement.Implementation
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly SupplyChainContext _context;

        public PurchaseOrderRepository(SupplyChainContext context)
        {
            _context = context;
        }

        public async Task<List<PurchaseOrder>> GetAll()
            => await _context.PurchaseOrders.ToListAsync();

        public async Task<PurchaseOrder?> GetById(long id)
            => await _context.PurchaseOrders.FindAsync(id);

        public async Task<PurchaseOrder> Create(PurchaseOrder po)
        {
            _context.PurchaseOrders.Add(po);
            await _context.SaveChangesAsync();
            return po;
        }

        public async Task<PurchaseOrder> Update(PurchaseOrder po)
        {
            _context.PurchaseOrders.Update(po);
            await _context.SaveChangesAsync();
            return po;
        }
        
public async Task<bool> Delete(long id)
{
    var po = await _context.PurchaseOrders.FindAsync(id);
    if (po == null)
    {
        return false;
    }

    _context.PurchaseOrders.Remove(po);
    await _context.SaveChangesAsync();
    return true;
}

public async Task Delete(PurchaseOrder po)
{
    _context.PurchaseOrders.Remove(po);
    await _context.SaveChangesAsync();
}

    }
}