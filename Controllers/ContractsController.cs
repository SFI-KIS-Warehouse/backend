using AbobaWH.Domain;
using AbobaWH.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AbobaWH.Controllers;

[Route("[controller]")]
[ApiController]
public class ContractsController : ControllerBase
{
	private readonly AppContext db;

	[HttpGet]
	public IResult Get()
	{
		var contracts = db.Contracts.Select(item => ContractDTO.FromDomain(item));

		return Results.Json(contracts);
	}

	[HttpGet("{id:int}")]
	public IResult Get(int id)
	{
		var contract = db.Contracts.SingleOrDefault(item => item.Id == id);

		if (contract == null)
			return Results.NotFound();

		return Results.Json(ContractDTO.FromDomain(contract));
	}

	[HttpGet( "{id:int}/changeStatus" )]
	public IResult ChangeStatus(int id, int code)
	{
		if (!Enumerable.Range(0, 4).Contains(code))
			return Results.BadRequest();

		var contract = db.Contracts.SingleOrDefault(item => item.Id == id);
		
		if (contract == null)
			return Results.NotFound();

		var status = (ContractStatuses)code;

		contract.Status = status;

		db.SaveChanges();

		return Results.Ok();
	}

	[HttpPost("add")]
	public IResult Add(AddContractRequest request)
	{
		if (request.ProductInfo.Count < 1)
			return Results.BadRequest();

		var productIds = request.ProductInfo.Select(item => item.Product).ToList();

		var productsCount = db.Products.Count(item => productIds.Contains(item.Id));

		if (productIds.Count != productsCount)
			return Results.BadRequest();

		var provider = db.Providers.SingleOrDefault(item => item.Id == request.Provider);

		if (provider == null)
			return Results.BadRequest();

		var contractItems = request.ProductInfo
			.Select( item => new ContractItem(item.Product, item.Count, item.Price) )
			.ToList();

		var contract = new Contract(provider, contractItems);

		db.Contracts.Add(contract);
		db.SaveChanges();

		return Results.Text(contract.Id.ToString());
	}

	public ContractsController(AppContext db)
	{
		this.db = db;
	}
}
