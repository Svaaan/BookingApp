namespace Booking.Api.Entities
{
    public class BookingConfirmation
    {
        public int ReservationId { get; set; }
        public int ShowId { get; set; }
        public string BookerEmail { get; set; }
        public int BookedSeats { get; set; }
        public string MovieTitle { get; set; }
        public DateTime ReservationTime { get; set; } = DateTime.UtcNow;
        public int BookingNumber { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime ShowStart { get; set; }
        public DateTime ShowEnd { get; set; }

        public BookingConfirmation(Reservation reservation, Show show) 
        { 
   
            ReservationId = reservation.Id;
            ShowId = reservation.ShowId;
            BookerEmail = reservation.Booker.Email;
            BookedSeats = reservation.BookedSeats;
            ReservationTime = DateTime.UtcNow;
            ShowStart = show.StartTime;
            ShowEnd = show.EndTime;
            MovieTitle = show.Movie.Title;
            TotalCost = show.CalculateTotalCost(reservation.BookedSeats);
        }
    }
}
