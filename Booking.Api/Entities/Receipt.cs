namespace Booking.Api.Entities
{
    public class Receipt
    {
        public int ReservationId { get; set; }
        public int ShowId { get; set; }
        public string BookerEmail { get; set; }
        public int BookedSeats { get; set; }
        public DateTime ReservationTime { get; set; } = DateTime.UtcNow;
        public decimal TotalCost { get; set; }

        public Receipt(Reservation reservation, Show show)
        {
            ReservationId = reservation.Id;
            ShowId = reservation.ShowId;
            BookerEmail = reservation.BookerEmail;
            BookedSeats = reservation.BookedSeats;
            ReservationTime = reservation.ReservationTime;

            TotalCost = show.CalculateTotalCost(reservation.BookedSeats);
        }
    }
}
