
using Supplychain.Enums.Admin;
public class Supplier
{
    public int SupplierId { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public SupplierStatus Status { get; set; }

    public required string Email { get; set; }

    public required string Phone { get; set; }


}


