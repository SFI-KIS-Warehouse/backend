namespace AbobaWH.Domain;

public class Shipment
{
	public int Id { get; set; }

	public DateTimeOffset? Time { get; set; }

	public ShipmentStatuses Status { get; set; }

	public List<ShipmentEntry> ProductInfo { get; set; }

	private Shipment() { }
}
