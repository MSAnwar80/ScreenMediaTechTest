using HotelApp.DAL;
using HotelApp.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SeedController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public async Task SeedDatabase()
        {
            await _applicationDbContext.Database.EnsureCreatedAsync();

            var addHotelsTask = _applicationDbContext.Hotels.AddRangeAsync(
                BuildHotel("Hilton", new List<Room>
                {
                    BuildRoom(RoomType.Single, 1),
                    BuildRoom(RoomType.Double, 2),
                    BuildRoom(RoomType.Deluxe, 4),
                    BuildRoom(RoomType.Single, 1),
                    BuildRoom(RoomType.Double, 2),
                    BuildRoom(RoomType.Deluxe, 4),
                }),
                BuildHotel("Marriott", new List<Room>
                {
                    BuildRoom(RoomType.Single, 1),
                    BuildRoom(RoomType.Double, 2),
                    BuildRoom(RoomType.Deluxe, 3),
                    BuildRoom(RoomType.Double, 2),
                    BuildRoom(RoomType.Double, 2),
                    BuildRoom(RoomType.Double, 2),
                })
                ,
                BuildHotel("Ibis", new List<Room>
                {
                    BuildRoom(RoomType.Single, 1),
                    BuildRoom(RoomType.Double, 2),
                    BuildRoom(RoomType.Deluxe, 5),
                    BuildRoom(RoomType.Double, 2),
                    BuildRoom(RoomType.Deluxe, 5),
                    BuildRoom(RoomType.Deluxe, 5),
                })
            );

            var addCustomersTask = _applicationDbContext.Customers.AddRangeAsync(
                new Customer
                {
                    Name = "John Doe"
                },
                new Customer
                {
                    Name = "tony Stark"
                }
            );

            Task.WaitAll(addHotelsTask, addCustomersTask);

            await _applicationDbContext.SaveChangesAsync();
        }

        [HttpDelete]
        public void ResetDataBase()
        {
            _applicationDbContext.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(e => e.State = EntityState.Detached);

            _applicationDbContext.Database.EnsureDeleted();
            _applicationDbContext.Database.EnsureCreated();
        }

        private Hotel BuildHotel(string name, List<Room> rooms)
        {
            return new Hotel
            {
                Name = name,
                Rooms = rooms
            };
        }

        private Room BuildRoom(RoomType roomType, int capacity)
        {
            return new Room
            {
                RoomType = roomType,
                Capacity = capacity
            };
        }
    }
}