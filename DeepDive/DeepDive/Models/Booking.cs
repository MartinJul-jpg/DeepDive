using DeepDive.Data;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DeepDive.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        [ValidateNever]
        public List<Product_Booking> Product_Bookings { get; set; } = new List<Product_Booking>();
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public bool Finalized { get; set; }

        private DateOnly _start;
        public DateOnly Start
        {
            get
            {
                DurationInDays = _end.DayNumber - _start.DayNumber;
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
                DurationInDays = _end.DayNumber - _start.DayNumber;
                return _end;
            }
            set
            {
                _end = value;
                DurationInDays = _end.DayNumber - _start.DayNumber;
                CalculatePriceOfBooking();
            }
        }
        [Range(1, int.MaxValue, ErrorMessage = "Dage må ikke ligge i fortiden.")]
        public int DurationInDays { get; set; }
        public double TotalPrice { get; set; }
        [Range(typeof(bool), "false", "false", ErrorMessage = $"Mængden af en vare overskrider lager på en af de pågældende datoer")]
        public bool ExceedsStock { get; set; }
        public Confirmation? ConfirmationSlip { get; set; }
        public int ConfirmationId { get; set; }

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
