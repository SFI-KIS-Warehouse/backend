namespace AbobaWH.Domain;

public class ReceiptOrderEntry
{
	public int Id { get; set; }

	public DeliveryScheduleEntry ScheduledDelivery { get; set; }

	public int Count { get; set; }

	private ReceiptOrderEntry() { }
}
