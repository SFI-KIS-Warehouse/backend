namespace AbobaWH.Domain;

public class Unit
{
	public int Id { get; set; }
	public string Name { get; set; }
	public bool IsHidden { get; set; }

	public Unit(string name)
	{
		Name = name;
	}

	protected Unit(int id, string name, bool isHidden)
	{
		Id = id;
		Name = name;
		IsHidden = isHidden;
	}
}
