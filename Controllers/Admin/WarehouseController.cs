using Microsoft.AspNetCore.Mvc;
using Supplychain.Data;
using Supplychain.DTOs.Admin;
using Supplychain.Models.Warehouse;
using System.Linq;

[ApiController]
[Route("api/v1/warehouses")]
public class WarehouseController : ControllerBase
{
    private readonly SupplyChainContext _context;

    public WarehouseController(SupplyChainContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllWarehouses([FromQuery] WarehouseSearchDto searchDto)
    {
        var query = _context.Warehouses.AsQueryable();

        if (!string.IsNullOrEmpty(searchDto?.Name))
            query = query.Where(x => x.Name.Contains(searchDto.Name));

        if (!string.IsNullOrEmpty(searchDto?.Location))
            query = query.Where(x => x.Location.Contains(searchDto.Location));

        if (searchDto?.Capacity != null)
            query = query.Where(x => x.Capacity == searchDto.Capacity);

        var warehouseList = query.ToList();
        return Ok(warehouseList);
    }

    [HttpPost]
    public IActionResult Add([FromBody] WarehouseDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid warehouse data");

        var warehouse = new Warehouse
        {
            Name = dto.Name,
            Location = dto.Location,
            Capacity = dto.Capacity
        };

        _context.Warehouses.Add(warehouse);
        _context.SaveChanges();

        return Ok(new WarehouseDto
        {
            Name = warehouse.Name,
            Location = warehouse.Location,
            Capacity = warehouse.Capacity
        });
    }

    [HttpDelete("{warehouseId}")]
    public IActionResult DeleteWarehouseById(int warehouseId)
    {
        var foundWarehouse = _context.Warehouses.Find(warehouseId);

        if (foundWarehouse == null || warehouseId <= 0)
        {
            return NotFound($"Warehouse with ID {warehouseId} not found");
        }

        _context.Warehouses.Remove(foundWarehouse);
        _context.SaveChanges();

        return Ok("Warehouse deleted successfully");
    }

    [HttpPut("{warehouseId}")]
    public IActionResult UpdateWarehouseById(int warehouseId, [FromBody] WarehouseDto dto)
    {
        var warehouse = _context.Warehouses.Find(warehouseId);

        if (warehouse == null || warehouseId <= 0)
            return NotFound("Warehouse not found");

        warehouse.Name = dto.Name;
        warehouse.Location = dto.Location;
        warehouse.Capacity = dto.Capacity;

        _context.SaveChanges();

        return Ok(new WarehouseDto
        {
            Name = warehouse.Name,
            Location = warehouse.Location,
            Capacity = warehouse.Capacity
        });
    }

    [HttpPatch("{warehouseId}")]
    public IActionResult PatchWarehouseById(int warehouseId, [FromBody] WarehousePatchDto dto)
    {
        var warehouse = _context.Warehouses.Find(warehouseId);

        if (warehouse == null || warehouseId <= 0)
            return NotFound("Warehouse not found");

        if (!string.IsNullOrEmpty(dto.Name))
            warehouse.Name = dto.Name;

        if (!string.IsNullOrEmpty(dto.Location))
            warehouse.Location = dto.Location;

        if (dto.Capacity.HasValue)
            warehouse.Capacity = dto.Capacity.Value;

        _context.SaveChanges();

        return Ok(new WarehouseDto
        {
            Name = warehouse.Name,
            Location = warehouse.Location,
            Capacity = warehouse.Capacity
        });
    }
}