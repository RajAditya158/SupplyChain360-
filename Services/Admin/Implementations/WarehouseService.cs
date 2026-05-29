using Supplychain.DTOs.Admin;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _repo;

    public WarehouseService(IWarehouseRepository repo)
    {
        _repo = repo;
    }

    public List<Warehouse> GetAllWarehouses() => _repo.GetAllWarehouses();

    public Warehouse AddWarehouse(Warehouse w) => _repo.Add(w);

    public bool DeleteWarehouseById(int warehouseId)
    {
        var warehouse = _repo.GetWarehouseById(warehouseId);

        if (warehouse == null)
        {
            return false;
        }

        _repo.DeleteWarehouseById(warehouseId);
        return true;
    }

    public List<Warehouse> SearchWarehouses(WarehouseSearchDto searchDto)
    {
        var query = _repo.GetAllWarehouses().AsQueryable();

        if (!string.IsNullOrEmpty(searchDto.Name))
            query = query.Where(x => x.Name.Contains(searchDto.Name));

        if (!string.IsNullOrEmpty(searchDto.Location))
            query = query.Where(x => x.Location.Contains(searchDto.Location));

        if (searchDto.Capacity != null)
            query = query.Where(x => x.Capacity == searchDto.Capacity);

        return query.ToList();
    }


}