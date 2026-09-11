using DeepDive.Repository;
using System.ComponentModel.DataAnnotations;

namespace DeepDive.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public List<int> BookedProducts { get; set; } = new List<int>();

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
                CalculatePriceOfBooking();
                DurationInDays = _end.DayNumber - _start.DayNumber;
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
                DurationInDays = _end.DayNumber - _start.DayNumber;
            }
        }
        private int _durationInDays;
        [Range(1, int.MaxValue)]
        public int DurationInDays { get; set; }
        public double TotalPrice { get; set; } 

       
        // TO DO: Make this a service or move it away from this class

        public void CalculatePriceOfBooking()
        {
            double totalPrice = 0;
            foreach (Product_Booking product_Booking in Product_Bookings)
            {
                Product product = product_Booking.Product;

                int daysBooked = _end.DayNumber - _start.DayNumber;

                totalPrice += daysBooked * product.Price;
            }

            TotalPrice = totalPrice;
        }
    }
}
