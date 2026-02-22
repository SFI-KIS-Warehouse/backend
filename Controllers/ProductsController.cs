using AbobaWH.Domain;
using AbobaWH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbobaWH.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
	private readonly AppContext db;

	[HttpGet]
	public IResult Get()
	{
		var products = db.Product.Include(product => product.Unit);

		return Results.Json(products);
	}

	[HttpGet("{id:int}")]
	public IResult Get(int id)
	{
		var product = db.Product
			.Include(product => product.Unit)
			.FirstOrDefault(item => item.Id == id);

		if (product == null)
			return Results.NotFound();

		return Results.Json(product);
	}

	[HttpGet("{id:int}/changeVisibility")]
	public IResult ChangeVisibility(int id, bool isHidden)
	{
		var product = db.Product.FirstOrDefault(item => item.Id == id);

		if (product == null)
			return Results.NotFound();

		product.IsHidden = isHidden;

		db.SaveChanges();

		return Results.Ok();
	}

	[HttpPost("add")]
	public IResult Add(AddProductRequest request)
	{
		var unit = db.Unit.FirstOrDefault(item => item.Id == request.Unit);

		if (unit == null)
			return Results.NotFound();

		var product = new Product(request.Name, unit, request.CriticalBalance);
		db.Product.Add(product);
		db.SaveChanges();

		return Results.Text(product.Id.ToString());
	}

	public ProductsController(AppContext db)
	{
		this.db = db;
	}

}
