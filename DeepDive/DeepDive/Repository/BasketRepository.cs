using DeepDive.Models;
using Microsoft.EntityFrameworkCore;

namespace DeepDive.Repository
{
    public class BasketRepository
    {
        private Booking _basket;

        public Booking GetBasket()
        {
            return _basket;
        }

        public void UpdateBasketDates(Booking basket)
        {
            _basket.Start = basket.Start;
            _basket.End = basket.End;
            _basket.ExceedsStock = basket.ExceedsStock;
        }

        public void UpdateBasketOverlap(bool exceedsStock)
        {
            _basket.ExceedsStock = exceedsStock;
        }

        public void clearBasket()
        {
            _basket = null;
        }
       

        public void ResetBasket()
        {
            _basket = new Booking();
            _basket.Start = DateOnly.FromDateTime(DateTime.Today);
            _basket.End = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
        }
    }
}
