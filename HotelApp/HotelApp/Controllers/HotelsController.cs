using HotelApp.DAL.Models;
using HotelApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Hotel>> GetHotel([FromRoute] string name)
        {
            var hotel = await _hotelService.GetHotelByName(name);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel([FromBody] Hotel hotel) // add request class
        {
            return await _hotelService.AddHotel(hotel);
        }
    }
}