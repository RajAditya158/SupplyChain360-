public class ShipmentDTO
{
    public long PoId { get; set; }
    public string Carrier { get; set; }
    public DateTime ExpectedDeliveryDate { get; set; }
    public string Status { get; set; }
}