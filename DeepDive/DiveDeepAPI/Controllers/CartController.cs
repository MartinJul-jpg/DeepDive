using Azure.Core;
using DiveDeepAPI.Models;
using DiveDeepAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

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
                ExceedsStock = _cartRepository.ValidateBookingOverlap(cart, userId).Result,

                ProductId = cart.Product_Bookings?.Select(pp => pp.ProductId).ToList() ?? new List<int>(),
            };
            return Ok(cartDTO);
        }
        
        [HttpGet("Booking/{userId}")]
        public async Task<IActionResult> GetUserBookings(string userId)
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
        public async Task<IActionResult> GetAllBookings(string userId)
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
        [HttpPost]
        public async Task<IActionResult> NewCart([FromBody] Booking booking)
        {
            if (booking == null) return BadRequest();
            else if (booking.Finalized == true) return BadRequest();

            //so user doesn't magically get 2 carts by refreshing the api call
            if (await _cartRepository.GetCart(booking.UserId) == null)
            {
                await _cartRepository.AddNewCart(booking);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{UserId}")]
        public async Task<IActionResult> CartToBooking(string UserId)
        {
            var cart = await _cartRepository.GetCart(UserId);
            if (cart == null) return BadRequest();
            else if (await _cartRepository.FinalizedCart(UserId) == true)
            {
                return Ok();
            }
            return Conflict();
            
        }
        [HttpPut("UpdateCart")]
        public async Task<IActionResult> UpdateCart([FromBody]Booking booking)
        {
            if (booking == null) return BadRequest();
            if (booking.Finalized == true) return BadRequest();

            var cart = await _cartRepository.GetCart(booking.UserId!);
            if (cart == null) BadRequest();

            await _cartRepository.UpdateCartBooking(cart!, booking);
            return Ok();
        }


        //Add Authorize here later 
        [HttpPut("UpdateBooking")]
        public async Task<IActionResult> UpdateBooking([FromBody]Booking booking)
        {
            if (booking == null) return BadRequest();
            if (booking.Finalized == false) return BadRequest();

            var confirmedBookings = await _cartRepository.GetConfirmedBookigns(booking.UserId!);
            var oldBooking = confirmedBookings.FirstOrDefault(b => b.BookingId == booking.BookingId);
            if (oldBooking == null) return BadRequest();

            await _cartRepository.UpdateCartBooking(oldBooking, booking);
            return Ok();
        }
        // Same here
        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            if (bookingId < 0) return BadRequest();
            else if (true == await _cartRepository.DeleteBooking(bookingId))
            {
                return Ok();
            }
            return Conflict();
        }
    }
}
