using AbobaWH.Domain;
using AbobaWH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbobaWH.Controllers;

[Route("[controller]")]
[ApiController]
public class ReceiptsController : ControllerBase
{
	private readonly AppContext db;

	[HttpGet]
	public IResult Get()
	{
		var receipts = db.ReceiptOrder
			.Include(receipt => receipt.ProductInfo!)
				.ThenInclude(orderEntry => orderEntry.ScheduledDelivery!.Contract.Provider)
			.Include(receipt => receipt.ProductInfo!)
				.ThenInclude(orderEntry => orderEntry.ScheduledDelivery!.Product)
					.ThenInclude(product => product.Unit)
			.Select(ReceiptOrderDTO.FromDomain);

		return Results.Json(receipts);
	}

	[HttpGet("{id:int}")]
	public IResult Get(int id)
	{
		var receipt = db.ReceiptOrder
			.Include(receipt => receipt.ProductInfo!)
				.ThenInclude(orderEntry => orderEntry.ScheduledDelivery!.Contract.Provider)
			.Include(receipt => receipt.ProductInfo!)
				.ThenInclude(orderEntry => orderEntry.ScheduledDelivery!.Product)
					.ThenInclude(product => product.Unit)
			.SingleOrDefault(item => item.Id == id);

		if (receipt == null)
			return Results.NotFound();

		return Results.Json( ReceiptOrderDTO.FromDomain(receipt) );
	}

	[HttpPost("add")]
	public IResult Add(AddReceiptRequestItem[] request)
	{
		var scheduledDeliveryIds = request
			.Select(item => item.ScheduledDelivery)
			.ToList();

		var scheduledDeliveries = db.DeliveryScheduleEntry
			.Include(item => item.Contract)
			.Where( item => scheduledDeliveryIds.Contains(item.Id) )
			.OrderBy(item => item.Id)
			.ToList();

		if (scheduledDeliveryIds.Count != scheduledDeliveries.Count)
			return Results.BadRequest();

		var receiptOrderEntries = request
			.OrderBy(item => item.ScheduledDelivery)
			.Select( (item, i) => new ReceiptOrderEntry(scheduledDeliveries[i], item.Count) )
			.ToList();

		var receiptOrders = new List<ReceiptOrder>();

		foreach (var group in receiptOrderEntries.GroupBy(item => item.ScheduledDelivery!.Contract.ProviderId))
		{
			var receiptOrder = new ReceiptOrder(DateTimeOffset.UtcNow, group);
			receiptOrders.Add(receiptOrder);
		}

		db.ReceiptOrder.AddRange(receiptOrders);
		db.SaveChanges();

		var receiptOrderIds = receiptOrders.Select(item => item.Id);

		return Results.Json(receiptOrderIds);
	}

	public ReceiptsController(AppContext db)
	{
		this.db = db;
	}
}
