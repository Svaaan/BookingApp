using Booking.Api.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Booking.Api.Repositories.Interfaces
{

    public interface IBookerRepository
    {
        Task<Booker> CreateBookerAsync(Booker booker);
    }
}

