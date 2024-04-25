namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public class BookingConfirmation
    {
        public int ReservationId { get; set; }
        public int ShowId { get; set; }
        public string BookerEmail { get; set; }
        public int BookedSeats { get; set; }
        public DateTime ReservationTime { get; set; } = DateTime.UtcNow;
        public decimal TotalCost { get; set; }
   
        public BookingConfirmation(ReservationDTO reservation, ShowDTO show)
        {
            ReservationId = reservation.Id;
            ShowId = reservation.ShowId;
            BookerEmail = reservation.BookerEmail;
            BookedSeats = reservation.BookedSeats;
            ReservationTime = DateTime.UtcNow;
            //TotalCost = show.CalculateTotalCost(reservation.BookedSeats);
            TotalCost = 10;
        }
    }
}
