using DiveDeepAPI.Data;
using DiveDeepAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DiveDeepAPI.Models
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
               // CalculatePriceOfBooking();
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
                //CalculatePriceOfBooking();
            }
        }
        [Range(1, int.MaxValue, ErrorMessage = "Dage må ikke ligge i fortiden.")]
        public int DurationInDays { get; set; }
        public double TotalPrice { get; set; }
        public bool ExceedsStock { get; set; }

        public byte[]? RowVersion { get; set; }
       // The API Doesn't use it so idk if it is needed, cause it's just causing problems right now 
       //public void CalculatePriceOfBooking()
       //{
       //    double totalPrice = 0;
       //    foreach (Product_Booking product_Booking in Product_Bookings)
       //    {
       //        Product product = product_Booking.Product;
       //
       //        int daysBooked = DurationInDays;
       //
       //        totalPrice += daysBooked * product.Price;
       //    }
       //
       //    TotalPrice = totalPrice;
       //}
    }
}
