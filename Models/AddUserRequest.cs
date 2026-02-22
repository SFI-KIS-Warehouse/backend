using AbobaWH.Domain;
using System.Text.Json.Serialization;

namespace AbobaWH.Models;

public class AddUserRequest
{
	public string Login { get; set; } = string.Empty;
	public string Pass { get; set; } = string.Empty;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public UserRoles Role { get; set; }
}
