using System.Text.Json.Serialization;
using SupplyChain360.Enums.Procurement;

public class SearchInboundShipmentDto
{
    public int PoId { get; set; }
    public string? SupplierName { get; set; }
     
     [JsonConverter(typeof(JsonStringEnumConverter))]
    public ShipmentStatus? Status { get; set; }
}