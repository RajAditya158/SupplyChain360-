using System.ComponentModel.DataAnnotations;
using SupplyChain360.Enums.Warehouse;

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
        public string SKU { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        public int QuantityOnHand { get; set; }

        [Required]
        public int SafetyStock { get; set; }

        public InventoryStatus Status { get; set; }

        [StringLength(50)]
        public string StorageLocation { get; set; }

        public int PhysicalCount { get; set; }

        public DateTime LastUpdated { get; set; }

        public int ReorderLevel { get; set; }

    }
}