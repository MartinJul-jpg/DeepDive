using DeepDive.Repository;

namespace DeepDive.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public List<int> BookedProducts { get; set; }
        public DateOnly Start
        {
            get;
            set
            {
                TotalPriceOfBooking();
            }
        }
        public DateOnly End { get;
            set
            {
                TotalPriceOfBooking();
            }
        }
        public double TotalPrice { get; set; }

        public void TotalPriceOfBooking()
        {
            double totalPrice = 0;
            foreach (int productId in BookedProducts)
            {
                Product product = ProductRepository.GetProductById(productId);
                int daysBooked = End.CompareTo(Start);
                totalPrice += daysBooked * product.Price;
            }

            TotalPrice = totalPrice;
        }
    }
}
