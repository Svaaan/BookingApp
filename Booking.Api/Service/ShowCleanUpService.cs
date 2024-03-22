using Booking.Api.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Api.Service
{
    public class ShowCleanUpService : BackgroundService
    {
        private readonly IShowRepository _showRepository;

        public ShowCleanUpService(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _showRepository.DeleteOverdueShows();

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
