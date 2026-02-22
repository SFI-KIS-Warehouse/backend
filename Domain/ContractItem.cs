using Microsoft.EntityFrameworkCore;

namespace AbobaWH.Domain;

public class ContractItem
{
	public int Id { get; set; }
	
	public Product? Product { get; set; }

	public int ProductId { get; set; }

	public int Count { get; set; }

	[Precision(15, 2)]
	public decimal Price { get; set; }

	public ContractItem(int productId, int count, decimal price)
	{
		ProductId = productId;
		Count = count;
		Price = price;
	}
}
