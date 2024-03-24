using Booking.Api.Controllers;
using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Entities.DTO;
using Booking.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Booking.Api.Repositories.Tests
{
    public class ReservationServiceTests
    {
        [Fact]
        public async Task CreateReservation_WithValidData_CreatesReservation()
        {
            var booker = new Booker { 
            Id = 123, Email = "test@example.com"
            };
            // Arrange
            var reservation = new CreateReservationDTO
            {
                ShowId = 1,
               BookerId = booker.Id,
                BookedSeats = 5
            };

            var mockRepository = new Mock<IReservationRepository>();
            mockRepository.Setup(repo => repo.CreateReservation(It.IsAny<Reservation>()))
                          .ReturnsAsync(new Reservation
                          {
                              ShowId = reservation.ShowId,
                              Booker = booker,
                              BookedSeats = reservation.BookedSeats
                          });

            var controller = new ReservationController(Mock.Of<ILogger<ReservationController>>(), mockRepository.Object);

            // Act
            var result = await controller.CreateReservation(reservation);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result.Result);
            var createdReservation = Assert.IsType<Reservation>(createdResult.Value);

            Assert.Equal(reservation.ShowId, createdReservation.ShowId);
            Assert.Equal(booker, booker);
            Assert.Equal(reservation.BookedSeats, createdReservation.BookedSeats);
        }


        [Fact]
        public async Task UpdateReservation_IncreasesAvailableSeats_WhenBookingSeatsDecrease()
        {
            // Arrange
            var booker = new Booker
            {
                Id = 123,
                Email = "test@example.com"
            };
            var reservationId = 1;
            var updateReservationDto = new ReservationDto
            {
                ShowId = 1,
                Booker = booker,
                BookedSeats = 5
            };

            var existingReservation = new Reservation
            {
                Id = reservationId,
                ShowId = 1,
                Booker = booker,
                BookedSeats = 8
            };

            var existingShow = new Show
            {
                Id = 1,
                AvailableSeats = 10
            };

            var mockContext = new Mock<CinemaDbContext>();
            mockContext.Setup(c => c.reservations.FindAsync(It.IsAny<int>())).ReturnsAsync(existingReservation);
            mockContext.Setup(c => c.shows.FindAsync(It.IsAny<int>())).ReturnsAsync(existingShow);

            var mockLogger = new Mock<ILogger<ReservationRepository>>();
            var reservationRepository = new ReservationRepository(mockContext.Object, mockLogger.Object);

            // Act
            await reservationRepository.UpdateReservation(reservationId, updateReservationDto);

            // Assert
            // For UpdateReservation_IncreasesAvailableSeats_WhenBookingSeatsDecrease
            Assert.Equal(expected: 13, existingShow.AvailableSeats);

            // Verify that SaveChangesAsync was called
            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void CalculateTotalCost_ReturnsCorrectTotalCost()
        {
            var booker = new Booker
            {
                Id = 123,
                Email = "test@example.com"
            };
            // Arrange
            var show = new Show
            {
                PricePerSeat = 10,
                InterestRate = 0.25m
            };

            var reservation = new Reservation
            {
                Id = 1,
                ShowId = 1,
                Booker = booker,
                BookedSeats = 3
            };

            // Act
            var totalCost = show.CalculateTotalCost(reservation.BookedSeats);

            var expectedTotalCost = reservation.BookedSeats * show.PricePerSeat * (1 + show.InterestRate);

            // Assert
            Assert.Equal(expectedTotalCost, totalCost);
        }

        [Fact]
        public async Task DeleteOverdueShows_RemovesOverdueShowsFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new CinemaDbContext(options))
            {
                var today = DateTime.Today;

                var overdueShows = new[]
                {
                   new Show { Id = 1, StartTime = today.AddDays(-2), EndTime = today.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59) }, // Overdue show
                   new Show { Id = 2, StartTime = today.AddDays(-1), EndTime = today.AddHours(1) } // Not overdue show
                };

                await context.shows.AddRangeAsync(overdueShows);
                await context.SaveChangesAsync();
            }

            using (var context = new CinemaDbContext(options))
            {
                var repository = new ShowRepository(context, Mock.Of<ILogger<ShowRepository>>());

                // Act
                await repository.DeleteOverdueShows();

                // Assert
                var remainingShows = await context.shows.ToListAsync();
                Assert.DoesNotContain(remainingShows, s => s.Id == 1);
            }


        }
        // in case we need to mock the database ;)
        private static DbSet<T> MockDbSet<T>(IQueryable<T> data)
            where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet.Object;
        }
    }
}
