using System.ComponentModel.DataAnnotations;

public class PurchaseOrder
{
    [Key]
    public long PoId { get; set; }
    public long SupplierId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ExpectedDeliveryDate { get; set; }
    public string Status { get; set; }
}
