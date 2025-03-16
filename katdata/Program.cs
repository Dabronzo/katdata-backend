using System.Text;
using katdata.Features.Entities.Population;
using katdata.Features.Models;
using katdata.Services;
using katdata.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//builder.Services.AddScoped(typeof(Repository<User, Guid>), typeof(MartenRepository<User, Guid>));

//builder.Services.AddScoped<ExampleRunningPop>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("Postgres");
var config = builder.Configuration;
var jwtSecret = config["Jwt:Secret"] ?? throw new InvalidOperationException("JWT secret is missing!");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();



if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("PostgreSQL connection string is missing!");
}

builder.Services.AddDbContext<Context>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped(typeof(Repository<,>), typeof(EfCoreRepository<,>));
//builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<GameSetUp>();


//builder.Services.AddMarten(options =>
//{
//    options.Connection(connectionString);


//    // Automatically create/update tables
//    options.AutoCreateSchemaObjects = AutoCreate.All;

//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Opens Swagger at the root URL
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
