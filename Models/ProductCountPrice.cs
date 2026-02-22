namespace AbobaWH.Models;

public class ProductCountPrice
{
	public int Product { get; set; }
	public int Count { get; set; }
	public decimal Price { get; set; }

	public ProductCountPrice(int product, int count, decimal price)
	{
		Product = product;
		Count = count;
		Price = price;
	}
}
