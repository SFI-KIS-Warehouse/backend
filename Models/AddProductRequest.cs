namespace AbobaWH.Models;

public class AddProductRequest
{
	public string Name { get; set; } = null!;

	public int Unit { get; set; }

	public int CriticalBalance { get; set; }
}
