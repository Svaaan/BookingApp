namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public class BookingConfirmationDTO
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


        public BookingConfirmationDTO(ReservationDTO reservation, ShowDTO show)
        {
            ReservationId = reservation.Id;
            ShowId = reservation.ShowId;
            BookerEmail = reservation.BookerEmail;
            BookedSeats = reservation.BookedSeats;
            ReservationTime = DateTime.UtcNow;
            ShowStart = show.StartTime;
            ShowEnd = show.EndTime;
            MovieTitle = show.MovieTitle;
          
           
        }
    }
}
