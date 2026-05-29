using System.ComponentModel.DataAnnotations;

namespace Supplychain.Models.Warehouse
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [Required]
        public int WarehouseId { get; set; }

        [Required]
        [StringLength(50)]
        public required string SKU { get; set; }

        [Required]
        [StringLength(100)]
        public required string ProductName { get; set; }

        [Required]
        public int QuantityOnHand { get; set; }

        [Required]
        public int SafetyStock { get; set; }

        [StringLength(20)]
        public required string Status { get; set; }   // Active, Inactive, etc.

        [StringLength(50)]
        public required string StorageLocation { get; set; }

        public int PhysicalCount { get; set; }

        public DateTime LastUpdated { get; set; }

        public int ReorderLevel { get; set; }

    }
}