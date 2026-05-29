using Supplychain.DTOs.Admin;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _repo;

    public SupplierService(ISupplierRepository repo)
    {
        _repo = repo;
    }

    // ✅ GET ALL
    public List<Supplier> GetAllSuppliers()
    {
        return _repo.GetAllSuppliers();
    }

    // ✅ ADD
    public Supplier AddSupplier(Supplier dto)
    {
        var supplier = new Supplier
        {
            Name = dto.Name,
            Type = dto.Type,
            Status = dto.Status,
            Email = dto.Email,
            Phone = dto.Phone
        };

        return _repo.Add(supplier);
    }

    // ✅ GET BY ID
    public Supplier GetSupplierById(int supplierId)
    {
        return _repo.GetSupplierById(supplierId);
    }

    // ✅ DELETE
    public void DeleteSupplierById(int supplierId)
    {
        var supplier = _repo.GetSupplierById(supplierId);

        if (supplier == null)
            throw new InvalidOperationException("Supplier not found");

        _repo.Delete(supplier);
    }

    // ✅ UPDATE
    public void UpdateSupplierById(int supplierId, Supplier supplier)
    {
        var existingSupplier = _repo.GetSupplierById(supplierId);

        if (existingSupplier == null)
            throw new InvalidOperationException($"Supplier with id {supplierId} not found.");

        existingSupplier.Name = supplier.Name;
        existingSupplier.Type = supplier.Type;
        existingSupplier.Status = supplier.Status;
        existingSupplier.Email = supplier.Email;
        existingSupplier.Phone = supplier.Phone;

        _repo.Update(existingSupplier);
    }

    // ✅ SEARCH / FILTER
    public List<Supplier> SearchSuppliers(SupplierSearchDto searchDto)
    {
        var query = _repo.GetAllSuppliers().AsQueryable();

        if (!string.IsNullOrEmpty(searchDto.Name))
            query = query.Where(x => x.Name.Contains(searchDto.Name));

        if (!string.IsNullOrEmpty(searchDto.Email))
            query = query.Where(x => x.Email.Contains(searchDto.Email));

        if (searchDto.Status.HasValue)
            query = query.Where(x => x.Status == searchDto.Status.Value);

        if (!string.IsNullOrEmpty(searchDto.Type))
            query = query.Where(x => x.Type == searchDto.Type);

        return query.ToList();
    }
}