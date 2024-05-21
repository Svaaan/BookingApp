using Microsoft.EntityFrameworkCore;
using Booking.Api.Entities;

namespace Booking.Api.Data
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions _options) : base(_options) { }

        public DbSet<Company> company { get; set; }
        public DbSet<MovieTheatre> movieTheatres { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Booker> bookers { get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<Salon> salons { get; set; }
        public virtual DbSet<Show> shows { get; set; }
        public virtual DbSet<Reservation> reservations { get; set; }

        public CinemaDbContext() : base()
        {
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

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

            modelBuilder.Entity<Show>()
                .Property(e => e.VAT)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<Show>()
                .Property(s => s.PricePerSeat)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Show)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.ShowId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Role)
                .HasConversion(
                v => v.ToString(),
                v => (EmployeeRole)Enum.Parse(typeof(EmployeeRole), v));

            modelBuilder.Entity<Employee>().HasOne(u => u.Company);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Company>().HasData(new Company
            {
                Id = 1,
                CompanyName = "TestCompany",
                Email = "Test@mail.com",
                Adress = "Testgatan 123b",
                Country = "Sverige"
            });
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 2,
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
            modelBuilder.Entity<MovieTheatre>().HasData(new MovieTheatre
            {
                Id = 1,
                CompanyId = 1,
                Name = "TestTheatre",
                Adress = "Biogatan 12a"
            });
            var (hashedPassword, salt) = PasswordHashing.HashPassword("password");
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 1,
                CompanyId = 1,
                Name = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Password = hashedPassword,
                Salt = salt,
                Role = EmployeeRole.Admin
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 2,
                CompanyId = 1,
                Name = "Tess",
                LastName = "Doe",
                Email = "tess@example.com",
                Password = hashedPassword,
                Salt = salt,
                Role = EmployeeRole.Employee
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 3,
                CompanyId = 1,
                Name = "Richard",
                LastName = "Doe",
                Email = "Richard@example.com",
                Password = hashedPassword,
                Salt = salt,
                Role = EmployeeRole.Manager
            });
            modelBuilder.Entity<Salon>().HasData(new Salon
            {
                Id = 1,
                AvailableSeats = 30,
                MovieTheatreId = 1,
                Name = "Salon 1",
                Status = 0
            });
            modelBuilder.Entity<Booker>().HasData(new Booker
            {
                Id=1,
                Name = "John",
                LastName = "Doe",
                Email = "Does@mail.com",
                PhoneNumber = "1234567890",
                BookingNumber = "7890"
            });
            modelBuilder.Entity<Show>().HasData(new Show
            {
                Id = 1,
                MovieId = 2,
                SalonId = 1,
                AvailableSeats = 15,
                PricePerSeat = 120,
                VAT = 25,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddDays(15)
            });
            modelBuilder.Entity<Reservation>().HasData(new Reservation
            {
                Id = 1,
                ShowId = 1,
                BookedSeats = 2,
                ReservationTime = DateTime.UtcNow.AddHours(5),
                BookerId = 1,
            });
        }
    }
}
