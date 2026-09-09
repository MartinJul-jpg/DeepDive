namespace DeepDive.Models
{
    public class Product_Booking
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int BookingId { get; set; }

        public Booking Booking { get; set; }
    }
}
