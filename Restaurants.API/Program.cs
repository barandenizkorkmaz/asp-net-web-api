using Restaurants.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration); // Extension method. See references in the comments.

var app = builder.Build();

// Middleware

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
