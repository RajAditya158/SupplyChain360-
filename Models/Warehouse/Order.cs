using System.ComponentModel.DataAnnotations;

namespace Supplychain.Models.Warehouse
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [StringLength(50)]
        public string ShipmentId { get; set; }

        [Required]
        [StringLength(50)]
        public required string SKU { get; set; }

        [Required]
        [StringLength(100)]
        public required string ProductName { get; set; }

        [Required]
        [StringLength(100)]
        public required string CustomerName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [StringLength(200)]
        public required string Address { get; set; }

        [Required]
        [StringLength(15)]
        public required string MobileNumber { get; set; }

        public DateTime ETA { get; set; }


        public string? Status { get; set; }

        public DateTime OrderDate { get; set; }
    }
}