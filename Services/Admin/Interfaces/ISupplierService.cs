using Supplychain.DTOs.Admin;

public interface ISupplierService
{
    List<Supplier> GetAllSuppliers();

    Supplier GetSupplierById(int supplierId);

    void UpdateSupplierById(int supplierId, Supplier supplier);

    Supplier AddSupplier(Supplier s);
    void DeleteSupplierById(int supplierId);
    List<Supplier> SearchSuppliers(SupplierSearchDto searchDto);


}