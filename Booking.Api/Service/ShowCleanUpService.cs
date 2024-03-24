using Booking.Api.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Api.Service
{
    public class ShowCleanUpService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ShowCleanUpService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var showRepository = scope.ServiceProvider.GetRequiredService<IShowRepository>();
                    await showRepository.DeleteOverdueShows();
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
