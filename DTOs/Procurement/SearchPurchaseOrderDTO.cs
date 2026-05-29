using System.Text.Json.Serialization;
using SupplyChain360.Enums.Procurement;

public class SearchByPurchaseDTO
{
    public string? SupplierName { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
     
     [JsonConverter(typeof(JsonStringEnumConverter))]
    public PurchaseOrderStatus? Status { get; set; }
}