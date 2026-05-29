namespace Supplychain.DTOs.Admin
{
    public class WarehouseDto
    {
        public required string Name { get; set; }
        public required string Location { get; set; }
        public int Capacity { get; set; }
    }
}
