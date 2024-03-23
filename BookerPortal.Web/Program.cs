using BookerPortal.Web.Components;
using Booking.Api.Data;
using Microsoft.EntityFrameworkCore;
using Request.HTTP.RequestService.IRequestService;
using Request.HTTP.RequestService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CinemaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CinemaDbContext"));
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddScoped<HttpClient>();

builder.Services.AddScoped<IBookerService, BookerService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ISalonService, SalonService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IShowService, ShowService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
