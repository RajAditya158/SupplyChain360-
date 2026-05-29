using System;
using System.ComponentModel.DataAnnotations;

namespace Supplychain.Dtos.Warehouse
{
    public class OrderCreateDto
    {
        [Required]
        public required string CustomerName { get; set; }

        [Required]
        public required string SKU { get; set; }

        [Required]
        public required string ProductName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public required string MobileNumber { get; set; }

        [Required]
        public DateTime ETA { get; set; }
    }
}
