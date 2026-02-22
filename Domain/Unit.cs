namespace AbobaWH.Domain;

public class Unit
{
	public int Id { get; set; }
	public string Name { get; set; }

	public Unit(string name)
	{
		Name = name;
	}

	protected Unit(int id, string name)
	{
		Id = id;
		Name = name;
	}
}
