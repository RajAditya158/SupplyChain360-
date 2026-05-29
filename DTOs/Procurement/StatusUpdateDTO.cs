using System.Text.Json.Serialization;
using SupplyChain360.Enums.Procurement;

public class StatusUpdateDTO
{
     [JsonConverter(typeof(JsonStringEnumConverter))]
    public ShipmentStatus Status { get; set; }
}