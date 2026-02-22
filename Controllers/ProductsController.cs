using AbobaWH.Domain;
using AbobaWH.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbobaWH.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
	private readonly AppContext db;

	[HttpGet]
	public IResult Get()
	{
		var products = db.Products;

		return Results.Json(products);
	}

	[HttpGet("{id:int}")]
	public IResult Get(int id)
	{
		var product = db.Products.FirstOrDefault(item => item.Id == id);

		if (product == null)
			return Results.NotFound();

		return Results.Json(product);
	}

	[HttpPost("add")]
	public IResult Add(AddProductRequest request)
	{
		var unit = db.Units.FirstOrDefault(item => item.Id == request.Unit);

		if (unit == null)
			return Results.NotFound();

		var product = new Product(request.Name, unit, request.CriticalBalance);
		db.Products.Add(product);
		db.SaveChanges();

		return Results.Text(product.Id.ToString());
	}

	public ProductsController(AppContext db)
	{
		this.db = db;
	}

}
