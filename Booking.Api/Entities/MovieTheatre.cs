namespace Booking.Api.Entities
{
    public class MovieTheatre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Show> Shows { get; set; }
    }
}
