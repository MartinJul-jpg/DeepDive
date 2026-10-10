using DiveDeepAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;
using Shared.Models;
using Shared.Services;

namespace DiveDeepAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        private readonly IProductRepository _productRepository;

        private readonly IDTO_Service _DTO_Service;

        public CartController(ICartRepository cartRepository, IDTO_Service DTO_Service)
        {
            _cartRepository = cartRepository;
            _DTO_Service = DTO_Service;
        }

        // GET localhost:8080/api/Cart/[userId]
        // returns users basket, is the only place validatebookingoverlap is called, hence it must be called after date or products have been changed
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserCart(string userId)
        {
            Booking cart = await _cartRepository.GetCart(userId);

            if (cart == null) return NotFound();

            List<ProductDTO> productDTOs = new List<ProductDTO>();
            foreach (Product_Booking pb in cart.Product_Bookings)
            {
                productDTOs.Add(_DTO_Service.ProductToDTO(pb.Product));
            }

            var cartDTO = new BookingDTO()
            {
                BookingId = cart.BookingId,
                UserId = cart.UserId,
                RowVersion = cart.RowVersion,
                Start = cart.Start,
                End = cart.End,

                //ProductIds = cart.Product_Bookings?.Select(pp => pp.ProductId).ToList() ?? new List<int>(),
                ProductsDTOs = productDTOs
            };
            return Ok(cartDTO);
        }

        [HttpGet("Booking/{bookingId}")]
        public async Task<IActionResult> GetBookingById(int bookingId)
        {
            Booking bookings = await _cartRepository.GetBookingById(bookingId);

            if (bookings == null) return NotFound();

            var bookingDTOs = new BookingDTO()
            {
                BookingId = bookings.BookingId,
                UserId = bookings.UserId,
                RowVersion = bookings.RowVersion,
                Start = bookings.Start,
                End = bookings.End,

                ProductsDTOs = bookings.Product_Bookings?.Select(pb => _DTO_Service.ProductToDTO(pb.Product)).ToList() ?? new List<ProductDTO>()
            };
            return Ok(bookingDTOs);
        }

        // GET localhost:8080/api/Cart/Booking/[userId]
        // returns users finalized bookings
        [HttpGet("Bookings/{userId}")]
        public async Task<IActionResult> GetUserBookings(string userId)
        {
            var bookings = await _cartRepository.GetConfirmedBookings(userId);

            if (bookings == null) return NotFound();

            var bookingDTOs = bookings.Select(b => new BookingDTO
            {
                BookingId = b.BookingId,
                UserId = b.UserId,
                RowVersion = b.RowVersion,
                Start = b.Start,
                End = b.End,

                //ProductIds = b.Product_Bookings?.Select(pp => pp.ProductId).ToList() ?? new List<int>(),
                ProductsDTOs = b.Product_Bookings?.Select(pb => _DTO_Service.ProductToDTO(pb.Product)).ToList() ?? new List<ProductDTO>()
            });
            return Ok(bookingDTOs);
        }

        // GET localhost:8080/api/Cart/Bookings
        // returns finalized bookings
        [HttpGet("Bookings")]
        public async Task<IActionResult> GetAllConfirmedBookings()
        {
            var bookings = await _cartRepository.GetAllConfirmedBookigns();

            if (bookings == null) return NotFound();


            var bookingDTO = bookings.Select(b => new BookingDTO
            {
                BookingId = b.BookingId,
                UserId = b.UserId,
                RowVersion = b.RowVersion,
                Start = b.Start,
                End = b.End,

                //ProductIds = b.Product_Bookings?.Select(pp => pp.ProductId).ToList() ?? new List<int>(),
                ProductsDTOs = b.Product_Bookings?.Select(pb => _DTO_Service.ProductToDTO(pb.Product)).ToList() ?? new List<ProductDTO>()
            });
            return Ok(bookingDTO);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateCartAsync(string userId)
        {
            if (userId.IsNullOrEmpty())
            {
                return BadRequest();
            }

            Booking cart = await _cartRepository.GetCart(userId);

            if (cart != null)
            {
                return Ok();
            }

            Booking newCart = new Booking()
            {
                UserId = userId,
                Finalized = false,
                Start = DateOnly.FromDateTime(DateTime.Now),
                End = DateOnly.FromDateTime(DateTime.Now.AddDays(1))
            };

            await _cartRepository.AddNewCartAsync(newCart);

            return Ok();
        }

        // PUT localhost:8080/api/Cart/[UserId]
        // finalizes usercart, returns OK
        [HttpPut("{UserId}")]
        public async Task<IActionResult> CartToBooking(string UserId)
        {
            Booking cart = await _cartRepository.GetCart(UserId);
            if (cart == null) return BadRequest();
            else if (await _cartRepository.FinalizedCart(UserId))
            {
                return Ok();
            }
            return Conflict();
        }

        // PUT localhost:8080/api/Cart/UpdateCart
        // sent booking updates database usercart, returns OK
        [HttpPut("UpdateCart")]
        public async Task<IActionResult> UpdateCart([FromBody] BookingDTO DTO)
        {
            if (DTO == null) return BadRequest();

            Booking updatedCart = _DTO_Service.DTO_ToBooking(DTO);

            Booking oldCart = await _cartRepository.GetBookingById(DTO.BookingId);

            if (oldCart == null) return BadRequest();

            await _cartRepository.UpdateCartBooking(updatedCart, oldCart);
            return Ok();
        }

        [HttpPut("{ProductId}/{UserId}")]
        public async Task<IActionResult> AddProductCart(int productId, string userId)
        {
            if (productId < 1) return BadRequest();
            if (string.IsNullOrEmpty(userId)) return BadRequest();

            Booking updatedCart = await _cartRepository.GetCart(userId);

            foreach (Product_Booking pb in updatedCart.Product_Bookings)
            {
                pb.Product = null;
                pb.Booking = null;
            }

            Booking oldCart = new Booking() { Product_Bookings = new List<Product_Booking>(updatedCart.Product_Bookings) };

            Product_Booking productBooking = new Product_Booking
            {
                ProductId = productId,
                BookingId = updatedCart.BookingId
            };

            updatedCart.Product_Bookings.Add(productBooking);

            await _cartRepository.UpdateCartBooking(updatedCart, oldCart);
            return Ok();
        }

        //Add Authorize here later 

        // PUT localhost:8080/api/Cart/UpdateBooking
        // sent booking updates database booking, returns OK
        [HttpPut("UpdateBooking")]
        public async Task<IActionResult> UpdateBooking([FromBody]BookingDTO DTO)
        {
            if (DTO == null) return BadRequest();

            Booking updatedBooking = _DTO_Service.DTO_ToBooking(DTO);

            Booking oldBooking = await _cartRepository.GetBookingById(DTO.BookingId);

            if (oldBooking == null) return BadRequest();

            updatedBooking.Finalized = true;

            await _cartRepository.UpdateBooking(updatedBooking);
            return Ok();
        }

        // Same here

        // DELETE localhost:8080/api/Cart/[bookingId]
        // deletes database booking, returns OK
        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            if (bookingId < 0) return BadRequest();
            else if (await _cartRepository.DeleteBooking(bookingId))
            {
                return Ok();
            }
            return Conflict();
        }

        [HttpGet("Validate/{UserId}")]
        public async Task<IActionResult> ValidateUserCart(string userId)
        {
            if (await _cartRepository.HasBookingOverlap(userId))
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }
    }
}
