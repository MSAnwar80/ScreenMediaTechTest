using HotelApp.DAL;
using HotelApp.DAL.Models;

namespace HotelApp.Services;

public class RoomService : IRoomService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public RoomService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public IQueryable<Room> GetAvailableRooms(int occupants, DateTime checkInDate, DateTime checkOutDate)
    {
        var availableRooms = _applicationDbContext.Rooms.Where(r =>
            r.Capacity >= occupants && r.Bookings.All(b => b.CheckOut < checkInDate && checkOutDate > b.CheckIn));

        return availableRooms;
    }
}