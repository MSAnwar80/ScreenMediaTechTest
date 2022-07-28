using HotelApp.DAL.Models;
using HotelApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("{occupants}/{checkInDate}/{checkOutDate}")]
        public ActionResult<List<Room>> GetAvailableRooms([FromRoute] int occupants, DateTime checkInDate,
            DateTime checkOutDate) // the parameters should be in a request class with validation for Dates etc
        {
            return _roomService.GetAvailableRooms(occupants, checkInDate, checkOutDate).ToList();
        }
    }
}