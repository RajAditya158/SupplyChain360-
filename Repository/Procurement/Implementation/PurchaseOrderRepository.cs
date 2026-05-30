using Supplychain.Data;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Repository.Procurement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SupplyChain360.Enums.Procurement;

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

        public async Task<PurchaseOrder?> GetById(int PoId)
            => await _context.PurchaseOrders.FindAsync(PoId);

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
        
public async Task<bool> Delete(int PoId)
{
    var po = await _context.PurchaseOrders.FindAsync(PoId);
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
public async Task<List<PurchaseOrder>> Search(SearchByPurchaseDTO dto)
{
    var query = _context.PurchaseOrders.AsQueryable();

    // Supplier Name - EXACT MATCH
    if (!string.IsNullOrEmpty(dto.SupplierName))
    {
        query = query.Where(p =>
            p.SupplierName.ToLower() == dto.SupplierName.ToLower());
    }

    // From Date
    if (dto.FromDate.HasValue)
    {
        query = query.Where(p =>
            p.OrderDate >= dto.FromDate.Value);
    }

    // To Date
    if (dto.ToDate.HasValue)
    {
        query = query.Where(p =>
            p.OrderDate <= dto.ToDate.Value);
    }

    // Status
    if (dto.Status.HasValue)
    {
        query = query.Where(p =>
            p.Status == dto.Status.Value);
    }

    return await query.ToListAsync();
}
public async Task<List<Supplier>> GetSuppliersForLookup()
{
    // Assuming your context has a DbSet<Supplier> called Suppliers
    return await _context.Suppliers.ToListAsync();
}

    }
}