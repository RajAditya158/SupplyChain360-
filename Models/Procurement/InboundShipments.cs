using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SupplyChain360.Enums.Procurement;

public class InboundShipment
{
    [Key] 
    public int ShipmentId { get; set; }
    public int PoId { get; set; }
    public int SupplierId { get; set; }

    public string SupplierName { get; set; }
    public string Carrier { get; set; } = "Blue Dart";
    public DateTime ExpectedDeliveryDate { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ShipmentStatus Status { get; set; }
}