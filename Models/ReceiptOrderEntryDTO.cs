using AbobaWH.Domain;

namespace AbobaWH.Models;

public class ReceiptOrderEntryDTO
{
	public Product Product { get; set; }
	public int Count { get; set; }
	public int Contract { get; set; }

	public ReceiptOrderEntryDTO(Product product, int count, int contract)
	{
		Product = product;
		Count = count;
		Contract = contract;
	}
}
