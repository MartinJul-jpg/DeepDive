using DeepDive.Models;
using DiveDeepAPI.Models;
using DiveDeepAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeepAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserCart(string userId)
        {
            var cart = await _cartRepository.GetCart(userId);

            if (cart == null) return NotFound();


            var cartDTO = new BookingDTO()
            {
                BookingId = cart.BookingId,
                UserId = cart.UserId,
                User = cart.User,
                Finalized = cart.Finalized,
                Start = cart.Start,
                End = cart.End,

                ProductId = cart.Product_Bookings?.Select(pp => pp.ProductId).ToList() ?? new List<int>(),
            };
            return Ok(cartDTO);
        }
    }
}
