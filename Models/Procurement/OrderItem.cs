using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderItem
{
    [Key]
    public int ItemId { get; set; }
    public int PoId { get; set; }
    public string ItemName { get; set; }= string.Empty;
    public int Quantity { get; set; }
    public double Price { get; set; }
}
