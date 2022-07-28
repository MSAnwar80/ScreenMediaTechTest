using HotelApp.DAL;
using HotelApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Services;

public class HotelService : IHotelService
{
    private readonly ApplicationDbContext _context;

    public HotelService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Hotel?> GetHotelByName(string name)
    {
        return await _context.Hotels.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
    }

    public async Task<Hotel> AddHotel(Hotel hotel)
    {
        await _context.Hotels.AddAsync(hotel);
        await _context.SaveChangesAsync();
        return hotel;
    }
}