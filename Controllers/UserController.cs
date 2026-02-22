using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AbobaWH.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private IConfiguration config;

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

	public UserController(IConfiguration config)
	{
		this.config = config;
	}
}
