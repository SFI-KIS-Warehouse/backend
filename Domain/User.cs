namespace AbobaWH.Domain;

public class User
{
	public int Id { get; set; }

	public string Login { get; set; }

	public string HashedPass { get; set; }

	public UserRoles Role { get; set; }
}