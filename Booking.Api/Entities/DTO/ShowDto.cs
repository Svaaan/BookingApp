﻿namespace Booking.Api.Entities.DTO
{
    public class ShowDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int SalonId { get; set; }
        public string SalonName { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; } = DateTime.UtcNow;
    }
}
