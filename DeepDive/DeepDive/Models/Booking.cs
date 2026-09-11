using DeepDive.Repository;
using System.ComponentModel.DataAnnotations;

namespace DeepDive.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public List<Product_Booking> Product_Bookings { get; set; } = new List<Product_Booking>();

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
                DurationInDays = _end.DayNumber - _start.DayNumber;
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
                DurationInDays = _end.DayNumber - _start.DayNumber;
                CalculatePriceOfBooking();
            }
        }
        private int _durationInDays;
        [Range(1, int.MaxValue)]
        public int DurationInDays { get; set; }
        public double TotalPrice { get; set; } 

        public void CalculatePriceOfBooking()
        {
            double totalPrice = 0;
            foreach (Product_Booking product_Booking in Product_Bookings)
            {
                Product product = product_Booking.Product;

                int daysBooked = DurationInDays;

                totalPrice += daysBooked * product.Price;
            }

            TotalPrice = totalPrice;
        }
    }
}
