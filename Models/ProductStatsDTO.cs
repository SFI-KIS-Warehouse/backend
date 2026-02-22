namespace AbobaWH.Models;

public class ProductStatsDTO
{
	public int Product { get; set; }
	public int Count { get; set; }
	public decimal AverageCost { get; set; }

	public ProductStatsDTO(int product, int count, decimal averageCost)
	{
		Product = product;
		Count = count;
		AverageCost = averageCost;
	}
}
