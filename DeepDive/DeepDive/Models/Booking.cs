using DeepDive.Repository;

namespace DeepDive.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public List<int> BookedProducts { get; set; }
        private DateOnly _start;
        public DateOnly Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
                CalculatePriceOfBooking();
            }
        }
        private DateOnly _end;
        public DateOnly End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
                CalculatePriceOfBooking();
            }
        }
        public double TotalPrice { get; set; }

        public void CalculatePriceOfBooking()
        {
            double totalPrice = 0;
            foreach (int productId in BookedProducts)
            {
                Product product = ProductRepository.GetProductById(productId);

                int daysBooked = End.DayNumber - Start.DayNumber;

                totalPrice += daysBooked * product.Price;
            }

            TotalPrice = totalPrice;
        }

        public Booking()
        {
            BookedProducts = new List<int>();
        }
    }
}
