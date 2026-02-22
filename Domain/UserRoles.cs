namespace AbobaWH.Domain;

[Flags]
public enum UserRoles
{
	Admin = 1,
	Director = 2,
	Manager = 4,
	Storekeeper = 8
}
