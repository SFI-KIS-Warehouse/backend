using AbobaWH.Models;
using AbobaWH.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbobaWH.Controllers;

[Route("[controller]")]
[ApiController]
public class DeliveryScheduleController : ControllerBase
{
	private readonly AppContext db;

	[HttpGet]
	public IResult Get()
	{
		var receipts = db.ReceiptOrder
			.Include(item => item.ProductInfo)
			.SelectMany(item => item.ProductInfo!);

		var entries = db
			.DeliveryScheduleEntry
			.Include(scheduleEntry => scheduleEntry.Product)
				.ThenInclude(product => product.Unit)
			.GroupJoin(
				receipts,
				scheduleEntry => scheduleEntry.Id,
				receipt => receipt.ScheduledDeliveryId,
				(scheduleEntry, receipts) => new {
					ScheduleEntry = scheduleEntry,
					RelatedReceiptEntry = receipts.SingleOrDefault()
				}
			)
			.Select(item => DeliveryScheduleEntryDTO.FromDomain(item.ScheduleEntry, item.RelatedReceiptEntry));

		return Results.Json(entries);
	}

	[HttpGet("{id:int}")]
	public IResult Get(int id)
	{
		var receipts = db.ReceiptOrder
			.Include(item => item.ProductInfo)
			.SelectMany(item => item.ProductInfo!);

		var entry = db
			.DeliveryScheduleEntry
			.Include(scheduleEntry => scheduleEntry.Product)
				.ThenInclude(product => product.Unit)
			.GroupJoin(
				receipts,
				scheduleEntry => scheduleEntry.Id,
				receipt => receipt.ScheduledDeliveryId,
				(scheduleEntry, receipts) => new {
					ScheduleEntry = scheduleEntry,
					RelatedReceiptEntry = receipts.SingleOrDefault()
				}
			)
			.SingleOrDefault(item => item.ScheduleEntry.Id == id);

		if (entry == null)
			return Results.NotFound();

		return Results.Json(DeliveryScheduleEntryDTO.FromDomain(entry.ScheduleEntry, entry.RelatedReceiptEntry));
	}

	[HttpPost("addEntry")]
	public IResult AddEntry(AddDeliveryScheduleEntryRequest request)
	{
		var contract = db.Contract
			.Include(item => item.ProductInfo)
			.FirstOrDefault(item => item.Id == request.Contract);

		if (contract == null)
			return Results.BadRequest();

		var product = db.Product.FirstOrDefault(item => item.Id == request.Product);

		if (product == null)
			return Results.BadRequest();

		var contractContainsProduct = contract.ProductInfo.Any(item => item.ProductId == product.Id);

		if (!contractContainsProduct)
			return Results.BadRequest();

		var scheduleEntry = new DeliveryScheduleEntry(request.Date, contract, product, request.Count);

		db.DeliveryScheduleEntry.Add(scheduleEntry);

		db.SaveChanges();

		return Results.Text(scheduleEntry.Id.ToString());
	}

	public DeliveryScheduleController(AppContext db)
	{
		this.db = db;
	}
}
