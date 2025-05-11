
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Application;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .WithOrigins("https://localhost:7177")  // Specifičan origin
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();  // Omogući kolačiće
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate(); // SAMO PRIMENJUJE MIGRACIJE NA BAZU
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
        throw; // Fail fast u developmentu
    }
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("AllowAll");
app.UseInfrastructure();

app.MapControllers();
app.Run();
