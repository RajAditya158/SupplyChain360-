namespace Supplychain.DTOs.Admin
{
    using Supplychain.Enum;

    public class SupplierDto
    {
        public required string Name { get; set; }
        public required string Type { get; set; }
        public SupplierStatus Status { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
}
