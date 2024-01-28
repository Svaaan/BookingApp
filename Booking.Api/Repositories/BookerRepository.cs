using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Booking.Api.Repositories
{
    public class BookerRepository : IBookerRepository
    {
        private readonly CinemaDbContext _context;
        private readonly ILogger<BookerRepository> _logger;

        public BookerRepository(CinemaDbContext context, ILogger<BookerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Booker> CreateBookerAsync(Booker booker)
        {
            try
            {

                var createBooker = await _context.bookers.AddAsync(booker);
                await _context.SaveChangesAsync();
                return createBooker.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booker");
                throw;
            }
        }
    }
}