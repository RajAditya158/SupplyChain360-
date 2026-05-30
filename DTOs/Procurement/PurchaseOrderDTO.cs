using System.Text.Json.Serialization;
using SupplyChain360.Enums.Procurement;

public class PurchaseOrderDTO
{
    public SupplierNameSelection SupplierName { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ExpectedDeliveryDate { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PurchaseOrderStatus Status { get; set; }
}
