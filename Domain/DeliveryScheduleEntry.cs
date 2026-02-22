namespace AbobaWH.Domain;

public class DeliveryScheduleEntry
{
	public int ID { get; set; }
	public DateOnly Date { get; set; }

	public Contract Contract { get; set; }

	public Product Product { get; set; }

	public int Count { get; set; }

	private DeliveryScheduleEntry() { }


}
