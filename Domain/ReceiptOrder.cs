namespace AbobaWH.Domain;

public class ReceiptOrder
{
	public int Id { get; set; }

	public DateTimeOffset Time { get; set; }

	public List<ReceiptOrderEntry>? ProductInfo { get; set; }

	public ReceiptOrder(DateTimeOffset time, IEnumerable<ReceiptOrderEntry> productInfo)
	{
		Time = time;
		ProductInfo = productInfo.ToList();
	}

	protected ReceiptOrder(int id, DateTimeOffset time)
	{
		Id = id;
		Time = time;
	}
}
