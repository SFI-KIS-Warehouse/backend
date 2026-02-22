using AbobaWH.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AbobaWH.Controllers;

[Route("[controller]")]
[ApiController]
public class UnitsController : ControllerBase
{
	private readonly AppContext db;

	[HttpGet]
	public IResult Get()
	{
		var units = db.Units;

		return Results.Json(units);
	}

	[HttpGet("{id:int}")]
	public IResult Get(int id)
	{
		var unit = db.Units.SingleOrDefault(x => x.Id == id);

		if (unit == null)
			return Results.NotFound();

		return Results.Json(unit);
	}

	[HttpPost("add")]
	public IResult Add(string name)
	{
		if (db.Units.Any(item => item.Name == name))
			return Results.Conflict();

		var unit = new Unit(name);

		db.Units.Add(unit);
		db.SaveChanges();

		return Results.Text(unit.Id.ToString());
	}

	public UnitsController(AppContext db)
	{
		this.db = db;
	}
}