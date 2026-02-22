using AbobaWH.Domain;
using AbobaWH.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbobaWH.Controllers;

[Route("productStats")]
[ApiController]
public class StatsController : ControllerBase
{
	private readonly AppContext db;

	[HttpPost]
	public IResult Get(int[] productIds)
	{
		var products = db.Product
			.Where(item => productIds.Contains(item.Id))
			.ToList();

		if (productIds.Length != products.Count)
			return Results.BadRequest();

		var receipts = db.ReceiptOrder
			.Include(order => order.ProductInfo!)
			.ThenInclude(orderEntry => orderEntry.ScheduledDelivery!)
			.ThenInclude(scheduleEntry => scheduleEntry.Contract)
			.ThenInclude(contract => contract.ProductInfo)
			.SelectMany(order => order.ProductInfo!);

		var shipments = db.ShipmentOrder
			.Include(order => order.ProductInfo!)
			.Where(order => order.Status == ShipmentStatuses.Shipped)
			.SelectMany(order => order.ProductInfo!);

		var stats = products
			.GroupJoin(
				receipts,
				product => product.Id,
				orderEntry => orderEntry.ScheduledDelivery!.ProductId,
				(product, orderEntries) => new { product, receipts })
			.GroupJoin(
				shipments,
				pair => pair.product.Id,
				shipment => shipment.ProductId,
				(pair, shipments) => new ProductStatsDTO(
					pair.product.Id,
					pair.receipts.Sum(receipt => receipt.Count) - shipments.Sum(shipment => shipment.Count),
					pair.receipts.Sum(item => item.ScheduledDelivery!.Contract.ProductInfo.First(item => item.ProductId == pair.product.Id).Price) / pair.receipts.Sum(item => item.Count)
				)
			);

		return Results.Json(stats);
	}

	public StatsController(AppContext db)
	{
		this.db = db;
	}
}
