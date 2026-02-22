using AbobaWH.Domain;
using AbobaWH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AbobaWH.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	private IConfiguration config;
	private readonly AppContext db;

	[HttpGet("login")]
	public IResult Login(string login, string password)
	{
		var jwt = new JwtSecurityToken(
			config["JWTParams:ValidIssuer"],
			config["JWTParams:ValidAudience"],
			expires: DateTime.Now.AddDays(1),
			signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTParams:SigningKey"]!)), SecurityAlgorithms.HmacSha256)
		);

		return Results.Text(new JwtSecurityTokenHandler().WriteToken(jwt));
	}

	[HttpGet]
	public IResult Get()
	{
		var users = db.User.Select(UserDTO.FromDomain);

		return Results.Json(users);
	}

	[HttpPost("create")]
	public IResult Create(AddUserRequest request)
	{
		var userExists = db.User.Any(item => item.Login == request.Login);

		if (userExists)
			return Results.Conflict();

		var user = new User(request.Login, request.Pass, request.Role);

		db.User.Add(user);

		return Results.Ok();
	}

	[HttpGet("{id:int}/changeRole")]
	public IResult ChangeRole(int id, UserRoles newRole)
	{
		var user = db.User.SingleOrDefault(item => item.Id == id);

		if (user == null)
			return Results.NotFound();

		user.Role = newRole;

		db.SaveChanges();

		return Results.Ok();
	}

	[HttpGet("{id:int}/delete")]
	public IResult Delete(int id)
	{
		var user = db.User.SingleOrDefault(item => item.Id == id);

		if (user == null)
			return Results.NotFound();

		db.User.Remove(user);

		db.SaveChanges();

		return Results.Ok();
	}

	public UsersController(IConfiguration config, AppContext db)
	{
		this.config = config;
		this.db = db;
	}
}
