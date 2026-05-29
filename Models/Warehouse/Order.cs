using System.ComponentModel.DataAnnotations;
using SupplyChain360.Enums.Warehouse;

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
        public string SKU { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(15)]
        public string MobileNumber { get; set; }

        public DateTime ETA { get; set; }


        public OrderStatus Status { get; set; }

        public DateTime OrderDate { get; set; }
    }
}