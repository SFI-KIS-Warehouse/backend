namespace AbobaWH.Models;

public class AddContractRequest
{
	public int Provider { get; set; }

	public List<ProductCountPrice> ProductInfo { get; set; } = null!;
}
