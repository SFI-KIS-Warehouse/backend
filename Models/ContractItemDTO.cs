using AbobaWH.Domain;

namespace AbobaWH.Models;

public class ContractItemDTO
{
	public Product Product { get; set; }
	public int Count { get; set; }
	public decimal Price { get; set; }

	public ContractItemDTO(Product product, int count, decimal price)
	{
		Product = product;
		Count = count;
		Price = price;
	}
}
