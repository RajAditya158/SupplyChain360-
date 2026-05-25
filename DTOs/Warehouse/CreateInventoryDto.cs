using System;
using System.ComponentModel.DataAnnotations;

namespace Supplychain.Dtos.Warehouse
{
    public class CreateInventoryDto
    {
        [Required]
        public string SKU { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int QuantityOnHand { get; set; }

        public string StorageLocation { get; set; }
    }
}
