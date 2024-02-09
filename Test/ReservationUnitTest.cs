using System;
using System.Threading.Tasks;
using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Entities.DTO;
using Booking.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using FluentAssertions;

namespace Booking.Api.Repositories.Tests
{
    public class ReservationServiceTests
    {
        [Fact]
        public async Task UpdateReservation_IncreasesAvailableSeats_WhenBookingSeatsDecrease()
        {
            // Arrange
            var reservationId = 1;
            var updateReservationDto = new ReservationDto
            {
                ShowId = 1,
                BookerEmail = "test@example.com",
                BookedSeats = 5 // This number can be adjusted based on your scenario
            };

            var existingReservation = new Reservation
            {
                Id = reservationId,
                ShowId = 1,
                BookerEmail = "test@example.com",
                BookedSeats = 8 // Initial booked seats are greater than the update
            };

            var existingShow = new Show
            {
                ID = 1,
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
            Assert.Equal(expected: 13 ,existingShow.AvailableSeats);

            // Verify that SaveChangesAsync was called
            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void CalculateTotalCost_ReturnsCorrectTotalCost()
        {
            // Arrange
            var show = new Show
            {
                PricePerSeat = 10
            };

            var reservation = new Reservation
            {
                Id = 1,
                ShowId = 1,
                BookerEmail = "test@example.com",
                BookedSeats = 3
            };

            // Act
            var totalCost = show.CalculateTotalCost(reservation.BookedSeats);

            // Assert
            Assert.Equal(30, totalCost);
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
