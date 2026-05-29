public interface IWarehouseRepository
{
    List<Warehouse> GetAllWarehouses();
    Warehouse Add(Warehouse w);

    bool DeleteWarehouseById(int warehouseId);

    Warehouse GetWarehouseById(int warehouseId);
}