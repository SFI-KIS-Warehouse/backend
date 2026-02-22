using AbobaWH.Domain;
using Microsoft.EntityFrameworkCore;

namespace AbobaWH;

public class AppContext: DbContext
{
	public DbSet<User> Users => Set<User>();

	public DbSet<Product> Products => Set<Product>();

	public DbSet<Unit> Units => Set<Unit>();

	public DbSet<Provider> Providers => Set<Provider>();

	public DbSet<Contract> Contracts => Set<Contract>();

	public DbSet<DeliveryScheduleEntry> DeliveryScheduleEntries => Set<DeliveryScheduleEntry>();

	public DbSet<ReceiptOrder> ReceiptOrders => Set<ReceiptOrder>();

	public DbSet<Shipment> Shipments => Set<Shipment>();

	public AppContext(DbContextOptions<AppContext> options) : base(options)
	{
	}
}
