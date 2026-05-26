using System.ComponentModel.DataAnnotations;

public class InboundShipment
{
    [Key] 
    public long ShipmentId { get; set; }
    public long PoId { get; set; }
     public long SupplierId { get; set; }
    public string Carrier { get; set; } = "Blue Dart";
    public DateTime ExpectedDeliveryDate { get; set; }
    public string Status { get; set; }
}