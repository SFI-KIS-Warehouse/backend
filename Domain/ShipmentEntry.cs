using Microsoft.EntityFrameworkCore;

namespace AbobaWH.Domain;

public class ShipmentEntry
{
	public int Id { get; set; }

	public Product? Product { get; set; }

	public int ProductId { get; set; }

	public int Count { get; set; }

	[Precision(15, 2)]
	public decimal Price { get; set; }

	public ShipmentEntry(int productId, int count, decimal price)
	{
		ProductId = productId;
		Count = count;
		Price = price;
	}

	protected ShipmentEntry(int id, int productId, int count, decimal price)
	{
		Id = id;
		ProductId = productId;
		Count = count;
		Price = price;
	}
}
