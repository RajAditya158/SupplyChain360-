namespace Supplychain.DTOs.Admin
{
    using Supplychain.Enum;

    public class SupplierSearchDto
    {
        public string? Name { get; set; }

        public string? Email { get; set; }
        public SupplierStatus? Status { get; set; }

        public string? Type { get; set; }
    }
}