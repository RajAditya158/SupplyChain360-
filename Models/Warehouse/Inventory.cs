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
        public required string SKU { get; set; }

        [Required]
        [StringLength(100)]
        public required string ProductName { get; set; }

        [Required]
        public int QuantityOnHand { get; set; }

        [Required]
        public int SafetyStock { get; set; }

<<<<<<< HEAD
        [StringLength(20)]
        public required string Status { get; set; }   // Active, Inactive, etc.
=======
        public InventoryStatus Status { get; set; }
>>>>>>> 4fe117dd693cd12865222f5387bdc8ac6c4fd6e5

        [StringLength(50)]
        public required string StorageLocation { get; set; }

        public int PhysicalCount { get; set; }

        public DateTime LastUpdated { get; set; }

        public int ReorderLevel { get; set; }

    }
}