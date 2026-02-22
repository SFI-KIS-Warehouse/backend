using AbobaWH.Domain;
using AbobaWH.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbobaWH.Controllers;

[Route("[controller]")]
[ApiController]
public class ShipmentsController : ControllerBase
{
	private readonly AppContext db;

	[HttpGet]
	public IResult Get()
	{
		var shipments = db.ShipmentOrder
			.Include(shipment => shipment.ProductInfo!)
				.ThenInclude(shipmentEntry => shipmentEntry.Product!.Unit);

		return Results.Json(shipments);
	}

	[HttpGet("{id:int}")]
	public IResult Get(int id)
	{
		var shipment = db.ShipmentOrder
			.Include(shipment => shipment.ProductInfo!)
				.ThenInclude(shipmentEntry => shipmentEntry.Product!.Unit)
			.SingleOrDefault(item => item.Id == id);

		if (shipment == null)
			return Results.NotFound();

		return Results.Json(shipment);
	}

	[HttpGet("{id:int}/ship")]
	public IResult Ship(int id)
	{
		var shipment = db.ShipmentOrder
			.Include(shipment => shipment.ProductInfo)
			.SingleOrDefault(item => item.Id == id);

		if (shipment == null)
			return Results.NotFound();

		var productIds = shipment.ProductInfo!
			.Select(item => item.ProductId)
			.ToList();

		var receipts = db.ReceiptOrder
			.Include(order => order.ProductInfo!)
			.ThenInclude(orderEntry => orderEntry.ScheduledDelivery)
			.SelectMany(order => order.ProductInfo!);

		var shipments = db.ShipmentOrder
			.Include(order => order.ProductInfo!)
			.Where(order => order.Status == ShipmentStatuses.Shipped)
			.SelectMany(order => order.ProductInfo!);

		var balances = db.Product
			.Where(item => productIds.Contains(item.Id))
			.GroupJoin(
				receipts,
				product => product.Id,
				orderEntry => orderEntry.ScheduledDelivery!.ProductId,
				(product, orderEntries) => new { product, receipts })
			.GroupJoin(
				shipments,
				pair => pair.product.Id,
				shipment => shipment.ProductId,
				(pair, shipments) => new {
					Product = pair.product,
					Count = pair.receipts.Sum(receipt => receipt.Count) - shipments.Sum(shipment => shipment.Count)
				}
			);

		var newBalances = shipment.ProductInfo!.Join(
			balances,
			orderEntry => orderEntry.ProductId,
			balance => balance.Product.Id,
			(orderEntry, balance) => new
			{
				Product = orderEntry.Product,
				NewBalance = balance.Count - orderEntry.Count
			}
		);

		if (newBalances.Any(item => item.NewBalance < 0))
			return Results.Conflict();

		shipment.Status = ShipmentStatuses.Shipped;
		shipment.Time = DateTimeOffset.UtcNow;

		db.SaveChanges();

		return Results.Ok();
	}

	[HttpPost("add")]
	public IResult Add(List<ProductCountPrice> request)
	{
		var productIds = request.Select(item => item.Product).ToList();

		var productsCount = db.Product.Count(item => productIds.Contains(item.Id));

		if (productIds.Count != productsCount)
			return Results.BadRequest();

		var shipmentEntries = request
			.Select( item => new ShipmentEntry(item.Product, item.Count, item.Price) )
			.ToList();

		var shipment = new ShipmentOrder(shipmentEntries);

		db.ShipmentOrder.Add(shipment);

		return Results.Text(shipment.Id.ToString());
	}

	public ShipmentsController(AppContext db)
	{
		this.db = db;
	}
}
