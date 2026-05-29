using System;
using System.ComponentModel.DataAnnotations;

namespace Supplychain.Dtos.Warehouse
{
    public class CreateInventoryDto
    {
        [Required]
        public required string SKU { get; set; }

        [Required]
        public required string ProductName { get; set; }

        [Required]
        public int QuantityOnHand { get; set; }

        public required string StorageLocation { get; set; }
    }
}
