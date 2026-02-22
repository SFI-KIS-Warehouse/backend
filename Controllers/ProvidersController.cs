using AbobaWH.Domain;
using AbobaWH.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbobaWH.Controllers;

[Route("[controller]")]
[ApiController]
public class ProvidersController : ControllerBase
{
	private readonly AppContext db;

	[HttpGet]
	public IResult Get()
	{
		var providers = db.Provider;

		return Results.Json(providers);
	}

	[HttpGet("{id:int}")]
	public IResult Get(int id)
	{
		var provider = db.Provider.SingleOrDefault(item => item.Id == id);

		if (provider == null)
			return Results.NotFound();

		return Results.Json(provider);
	}

	[HttpGet("{id:int}/changeVisibility")]
	public IResult ChangeVisibility(int id, bool isHidden)
	{
		var provider = db.Provider.SingleOrDefault(item => item.Id == id);

		if (provider == null)
			return Results.NotFound();

		provider.IsHidden = isHidden;

		db.SaveChanges();

		return Results.Ok();
	}

	[HttpPost("add")]
	public IResult Add(AddProviderRequest request)
	{
		var provider = new Provider(
			request.Name, request.ITN, request.BIC, request.SettlementAccount,
			request.DirectorFullName, request.AccountantFullName
		);

		db.Provider.Add(provider);
		db.SaveChanges();

		return Results.Text(provider.Id.ToString());
	}

	public ProvidersController(AppContext db)
	{
		this.db = db;
	}
}
