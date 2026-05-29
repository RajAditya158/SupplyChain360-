using System.Text.Json.Serialization;
using SupplyChain360.Enums.Procurement;

public class ShipmentDTO
{
    public int PoId { get; set; }
    public string? Carrier { get; set; }
    public DateTime ExpectedDeliveryDate { get; set; }
    
     [JsonConverter(typeof(JsonStringEnumConverter))]
    public ShipmentStatus? Status { get; set; }
}