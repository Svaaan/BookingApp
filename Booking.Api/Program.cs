using Microsoft.EntityFrameworkCore;
using Booking.Api.Repositories;
using Booking.Api.Data;
using Booking.Api.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Include XML comments for Swagger documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<CinemaDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("CinemaDbContext")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
policy.WithOrigins("http://localhost:5226", "https://localhost:5226") //Change to you're own localhosts
.AllowAnyMethod()
.WithHeaders()
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
