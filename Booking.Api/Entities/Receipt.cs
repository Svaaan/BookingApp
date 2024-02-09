namespace Booking.Api.Entities
{
    public class Receipt
    {
        public int ReservattionId { get; set; }
        public int ShowId { get; set; }
        public string BookerEmail { get; set; }
        public int BookedSeats { get; set; }
        public DateTime ReservationTime { get; set; }
        public decimal TotalCost { get; set; }

        public Receipt(Reservation reservation, Show show)
        {
            ReservattionId = reservation.Id;
            ShowId = reservation.ShowId;
            BookerEmail = reservation.BookerEmail;
            BookedSeats = reservation.BookedSeats;
            ReservationTime = reservation.ReservationTime;

            TotalCost = show.CalculateTotalCost(reservation.BookedSeats);
        }
    }
}
