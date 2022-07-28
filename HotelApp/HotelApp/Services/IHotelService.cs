using HotelApp.DAL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HotelApp.Services;

public interface IHotelService
{
    Task<Hotel?> GetHotelByName(string name);
    Task<Hotel> AddHotel(Hotel hotel);
}