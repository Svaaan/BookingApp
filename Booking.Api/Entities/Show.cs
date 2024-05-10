namespace Booking.Api.Entities
{
    public class Show
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int SalonId { get; set; }
        public Salon Salon { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; } = DateTime.UtcNow;
        public decimal PricePerSeat { get; set; }
        public decimal VAT { get; set; } = 25;

        public ICollection<Reservation> Reservations { get; set; }

        public decimal CalculateTotalCost(int bookedSeats)
        {
            decimal totalCost = bookedSeats * PricePerSeat;

            if (VAT > 0)
            {
                decimal interestAmount = totalCost * VAT;
                totalCost += interestAmount;
            }

            return totalCost;
        }
    }
}
