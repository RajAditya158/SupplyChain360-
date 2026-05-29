using Supplychain.DTOs.Admin;

public interface IWarehouseService
{
    List<Warehouse> GetAllWarehouses();
    Warehouse AddWarehouse(Warehouse w);

    bool DeleteWarehouseById(int warehouseId);
    List<Warehouse> SearchWarehouses(WarehouseSearchDto searchDto);

}