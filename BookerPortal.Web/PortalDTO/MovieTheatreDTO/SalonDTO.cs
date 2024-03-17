﻿using System.Text.Json.Serialization;

namespace Booking.Api.Entities
{
    public class Salon
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int AvailableSeats { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
    }

    public enum Status
    {
        Open,
        Closed,
        FullBooked
    }
}