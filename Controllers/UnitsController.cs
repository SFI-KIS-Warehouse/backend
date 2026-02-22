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
		var units = db.Unit;

		return Results.Json(units);
	}

	[HttpGet("{id:int}")]
	public IResult Get(int id)
	{
		var unit = db.Unit.SingleOrDefault(x => x.Id == id);

		if (unit == null)
			return Results.NotFound();

		return Results.Json(unit);
	}

	[HttpGet("{id:int}/changeVisibility")]
	public IResult ChangeVisibility(int id, bool isHidden)
	{
		var unit = db.Unit.SingleOrDefault(x => x.Id == id);

		if (unit == null)
			return Results.NotFound();

		unit.IsHidden = isHidden;

		db.SaveChanges();

		return Results.Ok();
	}

	[HttpPost("add")]
	public IResult Add(string name)
	{
		if (db.Unit.Any(item => item.Name == name))
			return Results.Conflict();

		var unit = new Unit(name);

		db.Unit.Add(unit);
		db.SaveChanges();

		return Results.Text(unit.Id.ToString());
	}

	public UnitsController(AppContext db)
	{
		this.db = db;
	}
}