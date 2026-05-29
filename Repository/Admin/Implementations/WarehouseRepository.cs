using Supplychain.Data;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly SupplyChainContext _context;

    public WarehouseRepository(SupplyChainContext context)
    {
        _context = context;
    }

    public List<Warehouse> GetAllWarehouses() => _context.Warehouses.ToList();

    public Warehouse Add(Warehouse w)
    {
        _context.Warehouses.Add(w);
        _context.SaveChanges();
        return w;
    }

    public bool DeleteWarehouseById(int warehouseId)
    {
        var warehouse = _context.Warehouses.Find(warehouseId);

        if (warehouse == null)
        {
            return false;
        }

        _context.Warehouses.Remove(warehouse);
        _context.SaveChanges();

        return true;
    }

    public Warehouse GetWarehouseById(int warehouseId) => _context.Warehouses.Find(warehouseId);
}