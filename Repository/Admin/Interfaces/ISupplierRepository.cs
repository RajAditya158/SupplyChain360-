public interface ISupplierRepository
{
    List<Supplier> GetAllSuppliers();
    Supplier Add(Supplier s);
    void Update(Supplier existingSupplier);

    void Delete(Supplier supplier);

    Supplier GetSupplierById(int id);
}