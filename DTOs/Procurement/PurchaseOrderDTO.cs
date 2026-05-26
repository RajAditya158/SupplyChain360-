public class PurchaseOrderDTO
{
    public long SupplierId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ExpectedDeliveryDate { get; set; }
    public string Status { get; set; }
}
