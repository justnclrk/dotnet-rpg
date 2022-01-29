using dotnet_rpg.Data;
using dotnet_rpg.Services.CharacterService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using dotnet_rpg.Services.WeaponService;
using dotnet_rpg.Services.FightService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//get config file
ConfigurationManager configuration = builder.Configuration;

//get connection string from config file
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
  c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
  {
    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
    In = ParameterLocation.Header,
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey
  });
  c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Token").Value)),
    ValidateIssuer = false,
    ValidateAudience = false
  };
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IWeaponService, WeaponService>();
builder.Services.AddScoped<IFightService, FightService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
