namespace AbobaWH.Models;

public class AddProviderRequest
{
	public string Name { get; set; } = null!;
	public long INT { get; set; }
	public int BIC { get; set; }
	public ulong SettlementAccount { get; set; }
	public string DirectorFullName { get; set; } = null!;
	public string AccountantFullName { get; set; } = null!;
}
