using katdata.Features.Models;
using katdata.Services;
using katdata.Tools;
using Marten;
using Microsoft.AspNetCore.Identity;
using Weasel.Core;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped(typeof(Repository<User, Guid>), typeof(MartenRepository<User, Guid>));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("Postgres");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("PostgreSQL connection string is missing!");
}

builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
   

    // Automatically create/update tables
    options.AutoCreateSchemaObjects = AutoCreate.All;

});

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
