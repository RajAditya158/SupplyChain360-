using System.ComponentModel.DataAnnotations;

public class OrderItem
{
    [Key]
    public long ItemId { get; set; }
    public long PoId { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}