using Booking.Web.Components;
using Microsoft.EntityFrameworkCore;
using Request.Http.DTO.MovieTheatreDTO;
using Booking.Api.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CinemaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CinemaDbContext"));
});

  

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<BookerRequest>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
