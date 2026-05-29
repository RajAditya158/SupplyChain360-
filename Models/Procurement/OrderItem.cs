using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderItem
{
    [Key]
    public int ItemId { get; set; }
    public int PoId { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    [ForeignKey("PoId")]
    public PurchaseOrder PurchaseOrder { get; set; }
}
