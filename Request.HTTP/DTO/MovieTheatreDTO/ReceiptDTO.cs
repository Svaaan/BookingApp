namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public class ReceiptDTO
    {
        public int ReservationId { get; set; }
        public Show Show { get; set; }
        public string BookerEmail { get; set; }
        public int? BookedSeats { get; set; }
        public DateTime ReservationTime { get; set; }
        public decimal TotalCost { get; set; }
    }
}
