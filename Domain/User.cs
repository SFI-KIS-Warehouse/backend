namespace AbobaWH.Domain;

public class User
{
	public int Id { get; set; }

	public string Login { get; set; }

	public string HashedPass { get; set; }

	public UserRoles Role { get; set; }

	public User(string login, string hashedPass, UserRoles role)
	{
		Login = login;
		HashedPass = hashedPass;
		Role = role;
	}

	protected User(int id, string login, string hashedPass, UserRoles role)
	{
		Id = id;
		Login = login;
		HashedPass = hashedPass;
		Role = role;
	}
}