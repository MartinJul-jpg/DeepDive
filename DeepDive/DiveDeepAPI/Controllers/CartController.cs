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
                RowVersion = cart.RowVersion,
                Start = cart.Start,
                End = cart.End,

                ProductId = cart.Product_Bookings?.Select(pp => pp.ProductId).ToList() ?? new List<int>(),
            };
            return Ok(cartDTO);
        }
        [HttpGet("Booking/{userId}")]
        public async Task<IActionResult> GetUserBookings(String userId)
        {
            var bookings = await _cartRepository.GetConfirmedBookigns(userId);

            if (bookings == null) return NotFound();


            var bookingDTO = bookings.Select(b => new BookingDTO
            {
                BookingId = b.BookingId,
                UserId = b.UserId,
                User = b.User,
                Finalized = b.Finalized,
                RowVersion = b.RowVersion,
                Start = b.Start,
                End = b.End,

                ProductId = b.Product_Bookings?.Select(pp => pp.ProductId).ToList() ?? new List<int>(),
            });
            return Ok(bookingDTO);
        }
        [HttpGet("Bookings/{userId}")]
        public async Task<IActionResult> GetAllBookings(String userId)
        {
            var bookings = await _cartRepository.GetConfirmedBookigns(userId);

            if (bookings == null) return NotFound();


            var bookingDTO = bookings.Select(b => new BookingDTO
            {
                BookingId = b.BookingId,
                UserId = b.UserId,
                User = b.User,
                Finalized = b.Finalized,
                RowVersion = b.RowVersion,
                Start = b.Start,
                End = b.End,

                ProductId = b.Product_Bookings?.Select(pp => pp.ProductId).ToList() ?? new List<int>(),
            });
            return Ok(bookingDTO);
        }
        [HttpGet("Bookings")]
        public async Task<IActionResult> GetAllConfirmedBookings()
        {
            var bookings = await _cartRepository.GetAllConfirmedBookigns();

            if (bookings == null) return NotFound();


            var bookingDTO = bookings.Select(b => new BookingDTO
            {
                BookingId = b.BookingId,
                UserId = b.UserId,
                User = b.User,
                Finalized = b.Finalized,
                RowVersion = b.RowVersion,
                Start = b.Start,
                End = b.End,

                ProductId = b.Product_Bookings?.Select(pp => pp.ProductId).ToList() ?? new List<int>(),
            });
            return Ok(bookingDTO);
        }
        [HttpPost("{UserId}")]
        public async Task<IActionResult> NewCart([FromBody]Booking Booking, string UserId)
        {
            if (Booking == null) return BadRequest();
            else if (Booking.Finalized == true) return BadRequest();

            await _cartRepository.AddNewCart(Booking);
            return Ok();
        }

        [HttpPut("{UserId}")]
        public async Task<IActionResult> CartToBooking(string UserId, int CartId)
        {
            return NotFound();
        }
    }
}
