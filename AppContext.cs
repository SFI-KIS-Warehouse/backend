using AbobaWH.Domain;
using Microsoft.EntityFrameworkCore;

namespace AbobaWH;

public class AppContext: DbContext
{
	public DbSet<User> User => Set<User>();

	public DbSet<Product> Product => Set<Product>();

	public DbSet<Unit> Unit => Set<Unit>();

	public DbSet<Provider> Provider => Set<Provider>();

	public DbSet<Contract> Contract => Set<Contract>();

	public DbSet<DeliveryScheduleEntry> DeliveryScheduleEntry => Set<DeliveryScheduleEntry>();

	public DbSet<ReceiptOrder> ReceiptOrder => Set<ReceiptOrder>();

	public DbSet<ShipmentOrder> ShipmentOrder => Set<ShipmentOrder>();

	public AppContext(DbContextOptions<AppContext> options) : base(options)
	{
	}
}
