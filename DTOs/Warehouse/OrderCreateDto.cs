using System;
using System.ComponentModel.DataAnnotations;

namespace Supplychain.Dtos.Warehouse
{
    public class OrderCreateDto
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string SKU { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public DateTime ETA { get; set; }
    }
}
