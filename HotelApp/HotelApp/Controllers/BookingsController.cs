using HotelApp.DAL.Models;
using HotelApp.Models.Requests;
using HotelApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<ActionResult<Booking?>> PostBooking([FromBody] BookingRequest request)
        {
            return await _bookingService.CreateNewBooking(request);
        }

        [HttpGet("{bookingId}")]
        public ActionResult<Booking> GetBooking([FromRoute] int bookingId)
        {
            var booking = _bookingService.GetBooking(bookingId);

            if (booking == null)
            {
                return new NotFoundResult();
            }

            return booking;
        }
    }
}