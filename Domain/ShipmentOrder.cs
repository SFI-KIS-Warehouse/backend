namespace AbobaWH.Domain;

public class ShipmentOrder
{
	public int Id { get; set; }

	public DateTimeOffset? Time { get; set; }

	public ShipmentStatuses Status { get; set; }

	public List<ShipmentEntry>? ProductInfo { get; set; }

	public ShipmentOrder(List<ShipmentEntry> productInfo)
	{
		Status = ShipmentStatuses.Created;
		ProductInfo = productInfo;
	}

	protected ShipmentOrder(int id, DateTimeOffset? time, ShipmentStatuses status)
	{
		Id = id;
		Time = time;
		Status = status;
	}
}
