using AbobaWH.Domain;
using System.Text.Json.Serialization;

namespace AbobaWH.Models;

public class UserDTO
{
	public int Id { get; set; }

	public string Login { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public UserRoles Role { get; set; }

	public UserDTO(int id, string login, UserRoles role)
	{
		Id = id;
		Login = login;
		Role = role;
	}
}
