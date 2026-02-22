using AbobaWH.Domain;
using AbobaWH.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbobaWH.Controllers;

[Route("[controller]")]
[ApiController]
public class ContractsController : ControllerBase
{
	private readonly AppContext db;

	[HttpGet]
	public IResult Get()
	{
		var contracts = db.Contract
			.Include(contract => contract.Provider)
			.Include(contract => contract.ProductInfo)
				.ThenInclude(contractItem => contractItem.Product)
					.ThenInclude(product => product!.Unit)
			.Select(ContractDTO.FromDomain);

		return Results.Json(contracts);
	}

	[HttpGet("{id:int}")]
	public IResult Get(int id)
	{
		var contract = db.Contract
			.Include(contract => contract.Provider)
			.Include(contract => contract.ProductInfo)
				.ThenInclude(contractItem => contractItem.Product)
					.ThenInclude(product => product!.Unit)
			.SingleOrDefault(item => item.Id == id);

		if (contract == null)
			return Results.NotFound();

		return Results.Json(ContractDTO.FromDomain(contract));
	}

	[HttpGet( "{id:int}/changeStatus" )]
	public IResult ChangeStatus(int id, int code)
	{
		if (!Enumerable.Range(0, 4).Contains(code))
			return Results.BadRequest();

		var contract = db.Contract.SingleOrDefault(item => item.Id == id);
		
		if (contract == null)
			return Results.NotFound();

		var status = (ContractStatuses)code;

		contract.Status = status;

		db.SaveChanges();

		return Results.Ok();
	}

	[HttpGet("{id:int}/changeVisibility")]
	public IResult ChangeVisibility(int id, bool isHidden)
	{
		var contract = db.Contract.SingleOrDefault(item => item.Id == id);
		
		if (contract == null)
			return Results.NotFound();

		contract.IsHidden = isHidden;

		db.SaveChanges();

		return Results.Ok();
	}

	[HttpPost("add")]
	public IResult Add(AddContractRequest request)
	{
		if (request.ProductInfo.Count < 1)
			return Results.BadRequest();

		var productIds = request.ProductInfo.Select(item => item.Product).ToList();

		var productsCount = db.Product.Count(item => productIds.Contains(item.Id));

		if (productIds.Count != productsCount)
			return Results.BadRequest();

		var provider = db.Provider.SingleOrDefault(item => item.Id == request.Provider);

		if (provider == null)
			return Results.BadRequest();

		var contractItems = request.ProductInfo
			.Select( item => new ContractItem(item.Product, item.Count, item.Price) )
			.ToList();

		var contract = new Contract(provider, contractItems);

		db.Contract.Add(contract);
		db.SaveChanges();

		return Results.Text(contract.Id.ToString());
	}

	public ContractsController(AppContext db)
	{
		this.db = db;
	}
}
