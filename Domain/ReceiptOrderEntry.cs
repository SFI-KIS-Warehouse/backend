namespace AbobaWH.Domain;

public class ReceiptOrderEntry
{
	public int Id { get; set; }

	public DeliveryScheduleEntry? ScheduledDelivery { get; set; }

	public int ScheduledDeliveryId { get; set; }

	public int Count { get; set; }

	public int ReceiptOrderId { get; set; }

	public ReceiptOrderEntry(DeliveryScheduleEntry scheduleEntry, int count)
	{
		ScheduledDelivery = scheduleEntry;
		Count = count;
	}

	protected ReceiptOrderEntry(int id, int scheduledDeliveryId, int count, int receiptOrderId)
	{
		Id = id;
		ScheduledDeliveryId = scheduledDeliveryId;
		Count = count;
		ReceiptOrderId = receiptOrderId;
	}
}
