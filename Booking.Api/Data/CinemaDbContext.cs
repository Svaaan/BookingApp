using Microsoft.EntityFrameworkCore;
using Booking.Api.Entities;

namespace Booking.Api.Data
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions _options) : base(_options) { }

        public DbSet<Booker> bookers { get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<Salon> salon { get; set; }
        public DbSet<Show> shows { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .Property(e => e.Genre)
                .HasConversion(
                v => v.ToString(),
                v => (Genres)Enum.Parse(typeof(Genres), v));

            modelBuilder.Entity<Movie>()
                .Property(e => e.Language)
                .HasConversion(
                v => v.ToString(),
                v => (Languages)Enum.Parse(typeof(Languages), v));

            modelBuilder.Entity<Movie>()
                .Property(e => e.Subtitle)
                .HasConversion(
                v => v.ToString(),
                v => (Subtitles)Enum.Parse(typeof(Subtitles), v));

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                ID = 2,
                Title = "The Godfather",
                Description = "The aging patriarch of an organized crime dynasty in postwar New York City transfers control of his clandestine empire to his reluctant youngest son",
                Director = "Francis Ford Coppola",
                Hours = 2,
                Minutes = 55,
                Language = Languages.English,
                Subtitle = Subtitles.Swedish,
                Genre = Genres.Crime,
                ReleaseYear = 1972,
                AgeRestriction = 15,
                MaxShows = 5,
            });
            modelBuilder.Entity<Salon>().HasData(new Salon
            {
                ID = 1,
                Name = "Salon 1",
                NumberOfSeats = 30,
            });
        }
    }
}
