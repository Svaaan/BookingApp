using System;
using Booking.Api.Entities;

namespace Booking.Api.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie>CreateMovieAsync(Movie movie);
    }
}
