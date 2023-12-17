using Microsoft.EntityFrameworkCore;
using Booking.Api.Repositories;
using Booking.Api.Data;
using System.Reflection;
using Booking.Api.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Booking API", Version = "v1" });
    s.SchemaFilter<EnumFilter>();
});

builder.Services.AddDbContext<CinemaDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("CinemaDbContext")),
    ServiceLifetime.Scoped);
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

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
