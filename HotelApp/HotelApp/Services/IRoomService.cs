using HotelApp.DAL.Models;

namespace HotelApp.Services;

public interface IRoomService
{
    IQueryable<Room> GetAvailableRooms(int occupants, DateTime checkInDate, DateTime checkOutDate);
}