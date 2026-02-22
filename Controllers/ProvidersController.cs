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
		var providers = db.Providers;

		return Results.Json(providers);
	}

	[HttpGet("{id:int}")]
	public IResult Get(int id)
	{
		var provider = db.Providers.SingleOrDefault(item => item.Id == id);

		if (provider == null)
			return Results.NotFound();

		return Results.Json(provider);
	}

	[HttpPost("add")]
	public IResult Add(AddProviderRequest request)
	{
		var provider = new Provider(
			request.Name, request.INT, request.BIC, request.SettlementAccount,
			request.DirectorFullName, request.AccountantFullName
		);

		db.Providers.Add(provider);
		db.SaveChanges();

		return Results.Text(provider.Id.ToString());
	}

	public ProvidersController(AppContext db)
	{
		this.db = db;
	}
}
