using Supplychain.Data;

public class SupplierRepository : ISupplierRepository
{
    private readonly SupplyChainContext _context;

    public SupplierRepository(SupplyChainContext context)
    {
        _context = context;
    }

    public List<Supplier> GetAllSuppliers()
    {
        return _context.Suppliers.ToList();
    }

    public Supplier Add(Supplier s)
    {
        _context.Suppliers.Add(s);
        _context.SaveChanges();
        return s;
    }

    public Supplier GetSupplierById(int id)
    {
        return _context.Suppliers
            .FirstOrDefault(s => s.SupplierId == id);
    }

    public void Update(Supplier supplier)
    {
        _context.Suppliers.Update(supplier);
        _context.SaveChanges();
    }

    public void Delete(Supplier supplier)
    {
        _context.Suppliers.Remove(supplier);
        _context.SaveChanges();
    }

    public object GetById(int supplierId)
    {
        return _context.Suppliers
            .FirstOrDefault(s => s.SupplierId == supplierId);
    }
}