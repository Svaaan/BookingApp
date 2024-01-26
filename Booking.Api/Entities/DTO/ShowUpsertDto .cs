namespace Booking.Api.Entities.DTO
{
    public class ShowUpsertDto
    {
        public int MovieId { get; set; }
        public int SalonId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
