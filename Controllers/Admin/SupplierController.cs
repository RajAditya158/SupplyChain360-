using Microsoft.AspNetCore.Mvc;
using Supplychain.DTOs.Admin;
using Supplychain.Enums;

[ApiController]
[Route("api/v1/suppliers")]
public class SupplierController : ControllerBase
{
    private readonly ISupplierService _service;
    private readonly IAuditService _audit;

    public SupplierController(ISupplierService service, IAuditService audit)
    {
        _service = service;
        _audit = audit;
    }

    [HttpGet]
    public IActionResult GetAllSuppliers([FromQuery] SupplierSearchDto searchDto)
    {
        _audit.Log(null, "Viewed all suppliers");

        var supplierList = searchDto == null
            ? _service.GetAllSuppliers()
            : _service.SearchSuppliers(searchDto);

        return Ok(supplierList);
    }

    [HttpPost]
    public IActionResult AddSupplier([FromBody] SupplierDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid supplier data");

        var supplier = new Supplier
        {
            Name = dto.Name,
            Type = dto.Type,
            Status = dto.Status,
            Email = dto.Email,
            Phone = dto.Phone
        };

        var result = _service.AddSupplier(supplier);

        _audit.Log(null, $"Added supplier: {result.Name}");

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSupplierById(int id)
    {
        var foundSupplier = _service.GetSupplierById(id);

        if (foundSupplier == null)
            return NotFound("Supplier not found");

        _service.DeleteSupplierById(id);

        _audit.Log(null, $"Deleted supplier ID: {id}");

        return Ok(new { message = "Supplier Deleted Successfully" });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSupplierById(int id, [FromBody] SupplierDto dto)
    {
        var supplier = _service.GetSupplierById(id);

        if (supplier == null)
            return NotFound("Supplier not found");

        supplier.Name = dto.Name;
        supplier.Type = dto.Type;
        supplier.Status = dto.Status;
        supplier.Email = dto.Email;
        supplier.Phone = dto.Phone;

        _service.UpdateSupplierById(id, supplier);

        _audit.Log(null, $"Updated supplier: {supplier.Name}");

        return Ok("Supplier updated successfully");
    }

    [HttpGet("{id}")]
    public IActionResult GetSupplierById(int id)
    {
        var foundSupplier = _service.GetSupplierById(id);

        if (foundSupplier == null || id <= 0)
            return NotFound("Supplier not found");

        _audit.Log(null, $"Viewed supplier ID: {id}");

        return Ok(foundSupplier);
    }
}