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
                if (true)
                {
                    CalculatePriceOfBooking();
                }
                _start = value;
            }
        }
        private DateOnly _end;
        public DateOnly End {
            get
            {
                return _end;
            }
            set
            {
                if (true)
                {
                    CalculatePriceOfBooking();
                }
                _end = value;
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
    }
}
