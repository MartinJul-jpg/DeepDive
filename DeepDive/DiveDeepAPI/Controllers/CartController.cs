using Azure.Core;
using DiveDeepAPI.Services;
using DiveDeepAPI.DTO;
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

        private readonly IProductRepository _productRepository;

        private readonly IDTO_Service _DTO_Service;

        public CartController(ICartRepository cartRepository, IDTO_Service DTO_Service, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _DTO_Service = DTO_Service;
            _productRepository = productRepository;
        }

        // GET localhost:8080/api/Cart/[userId]
        // returns users basket, is the only place validatebookingoverlap is called, hence it must be called after date or products have been changed
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserCart(string userId)
        {
            var cart = await _cartRepository.GetCart(userId);

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
                Products = productDTOs
            };
            return Ok(cartDTO);
        }

        [HttpGet("Booking/{bookingId}")]
        public async Task<IActionResult> GetBookingById(int bookingId)
        {
            var bookings = await _cartRepository.GetBookingById(bookingId);

            if (bookings == null) return NotFound();

            var bookingDTOs = new BookingDTO()
            {
                BookingId = bookings.BookingId,
                UserId = bookings.UserId,
                RowVersion = bookings.RowVersion,
                Start = bookings.Start,
                End = bookings.End,

                Products = bookings.Product_Bookings?.Select(pb => _DTO_Service.ProductToDTO(pb.Product)).ToList() ?? new List<ProductDTO>()
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
                Products = b.Product_Bookings?.Select(pb => _DTO_Service.ProductToDTO(pb.Product)).ToList() ?? new List<ProductDTO>()
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
                Products = b.Product_Bookings?.Select(pb => _DTO_Service.ProductToDTO(pb.Product)).ToList() ?? new List<ProductDTO>()
            });
            return Ok(bookingDTO);
        }

        // PUT localhost:8080/api/Cart/[UserId]
        // finalizes usercart, returns OK
        [HttpPut("{UserId}")]
        public async Task<IActionResult> CartToBooking(string UserId)
        {
            var cart = await _cartRepository.GetCart(UserId);
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
        public async Task<IActionResult> UpdateCart([FromBody]BookingDTO DTO)
        {
            if (DTO == null) return BadRequest();

            Booking updatedCart = _DTO_Service.DTO_ToBooking(DTO);

            if (updatedCart.Finalized == true) return BadRequest();

            var cart = await _cartRepository.GetCart(updatedCart.UserId!);
            if (cart == null) BadRequest();

            await _cartRepository.UpdateCartBooking(updatedCart);
            return Ok();
        }

        [HttpPut("{ProductId}/{UserId}")]
        public async Task<IActionResult> AddProductCart(int ProductId, string UserId)
        {
            if (ProductId == null && ProductId <= 0) return BadRequest();
            if (string.IsNullOrEmpty(UserId)) return BadRequest();

            var updateCart = await _cartRepository.GetCart(UserId);
            if (updateCart == null) return NotFound();

            var productAdd = await _productRepository.GetProductById(ProductId);
            if (productAdd == null) return NotFound();

            var productBooking = new Product_Booking
            {
                Product = productAdd,
                ProductId = productAdd.ProductId,
                Booking = updateCart,
                BookingId = updateCart.BookingId
            };

            updateCart.Product_Bookings ??= new List<Product_Booking>();
            updateCart.Product_Bookings.Add(productBooking);

            await _cartRepository.UpdateCartBooking(updateCart);
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

            var confirmedBookings = await _cartRepository.GetConfirmedBookings(updatedBooking.UserId!);
            var oldBooking = confirmedBookings.FirstOrDefault(b => b.BookingId == updatedBooking.BookingId);
            if (oldBooking == null) return BadRequest();

            await _cartRepository.UpdateCartBooking(updatedBooking);
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
            if (await _cartRepository.ValidateBookingOverlap(userId))
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }
    }
}
