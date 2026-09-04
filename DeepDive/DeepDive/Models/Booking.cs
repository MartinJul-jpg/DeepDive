namespace DeepDive.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public List<int> BookedProducts { get; set; }
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }
    }
}
