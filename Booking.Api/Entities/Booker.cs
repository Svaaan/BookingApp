﻿namespace Booking.Api.Entities
{
    public class Booker
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? BookingNumber { get; set; }
    }
}
