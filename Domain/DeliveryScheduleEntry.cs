namespace AbobaWH.Domain;

public class DeliveryScheduleEntry
{
	public int Id { get; set; }

	public DateOnly Date { get; set; }

	public Contract Contract { get; set; }

	public int ContractId { get; set; }

	public Product Product { get; set; }

	public int ProductId { get; set; }

	public int Count { get; set; }

	public DeliveryScheduleEntry(DateOnly date, Contract contract, Product product, int count)
	{
		Date = date;
		Contract = contract;
		Product = product;
		Count = count;
	}

	protected DeliveryScheduleEntry(int id, DateOnly date, int productId, int count)
	{
		Id = id;
		Date = date;
		Contract = null!;
		Product = null!;
		ProductId = productId;
		Count = count;
	}
}
