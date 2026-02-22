namespace AbobaWH.Domain;

public class ReceiptOrder
{
	public int Id { get; set; }

	public DateTimeOffset Time { get; set; }

	public List<ReceiptOrderEntry> ProductInfo { get; set; }

	private ReceiptOrder() { }
}
