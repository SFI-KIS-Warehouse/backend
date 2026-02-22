namespace AbobaWH.Domain;

public class Provider
{
	public int Id { get; set; }
	public string Name { get; set; }
	public long ITN { get; set; }

	public int BIC { get; set; }

	public ulong SettlementAccount { get; set; }

	public string DirectorFullName { get; set; }

	public string AccountantFullName { get; set; }

	public Provider(
		string name, long itn, int bic,
		ulong settlementAccount,
		string directorFullName, string accountantFullName
	)
	{
		Name = name;
		ITN = itn;
		BIC = bic;
		SettlementAccount = settlementAccount;
		DirectorFullName = directorFullName;
		AccountantFullName = accountantFullName;
	}

	protected Provider(
		int id, string name, long ITN, int BIC,
		ulong settlementAccount,
		string directorFullName, string accountantFullName
	)
	{
		Id = id;
		Name = name;
		this.ITN = ITN;
		this.BIC = BIC;
		SettlementAccount = settlementAccount;
		DirectorFullName = directorFullName;
		AccountantFullName = accountantFullName;
	}
}
