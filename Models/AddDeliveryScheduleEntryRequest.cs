namespace AbobaWH.Models;

public class AddDeliveryScheduleEntryRequest
{
	public DateOnly Date { get; set; }

	public int Contract { get; set; }

	public int Product { get; set; }

	public int Count { get; set; }
}
