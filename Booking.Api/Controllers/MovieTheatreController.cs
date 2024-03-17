using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieTheatreController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<MovieTheatreController> _logger;

        public MovieTheatreController(ILogger<MovieTheatreController> logger)
        {
            _logger = logger;
        }
       
    }
}
