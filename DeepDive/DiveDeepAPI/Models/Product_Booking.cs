namespace DiveDeepAPI.Models
{
    public class Product_Booking
    {
        public int Product_BookingId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}
