using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AbobaWH;

public class Startup
{
	private IConfiguration config;

	public Startup(IConfiguration config)
	{
		this.config = config;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddControllers();

		var conStr = config["ConStr"];

		services.AddDbContext<AppContext>(options => options.UseNpgsql(conStr));

		services.AddCors(options => 
			{
				options.AddDefaultPolicy(builder => builder
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
				);
			}
		);

		var jwtValidationParams = new TokenValidationParameters()
		{
			ValidateIssuer = true,
			ValidIssuer = config["JWTParams:ValidIssuer"],
			ValidateAudience = true,
			ValidAudience = config["JWTParams:ValidAudience"],
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTParams:SigningKey"]!))
		};

		services
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options => options.TokenValidationParameters = jwtValidationParams);

		services.AddAuthorization();

	}

	public void Configure(WebApplication app, IWebHostEnvironment env)
	{
		app.UseAuthentication().UseAuthorization();

		app.MapControllers();

		app.UseCors();

		var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppContext>();
		context.Database.Migrate();
	}
}
