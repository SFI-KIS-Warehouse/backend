using Microsoft.EntityFrameworkCore;

namespace AbobaWH.Domain;

public class ShipmentEntry
{
	public int Id { get; set; }

	public Product Product { get; set; }

	public int Count { get; set; }

	[Precision(15, 2)]
	public decimal Price { get; set; }

	private ShipmentEntry() { }
}
