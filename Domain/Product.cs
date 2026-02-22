namespace AbobaWH.Domain;

public class Product
{
	public int Id { get; set; }
	public string Name { get; set; }
	public Unit Unit { get; set; }
	public int CriticalBalance { get; set; }
	public bool IsHidden { get; set; }

	public Product(string name, Unit unit, int criticalBalance)
	{
		Name = name;
		Unit = unit;
		CriticalBalance = criticalBalance;
	}

	protected Product(int id, string name, int criticalBalance, bool isHidden)
	{
		Id = id;
		Name = name;
		Unit = null!;
		CriticalBalance = criticalBalance;
		IsHidden = isHidden;
	}
}
