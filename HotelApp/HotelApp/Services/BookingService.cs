using HotelApp.DAL;
using HotelApp.DAL.Models;
using HotelApp.Models.Requests;

namespace HotelApp.Services;

public class BookingService : IBookingService
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IRoomService _roomService;

    public BookingService(ApplicationDbContext applicationDbContext, IRoomService roomService)
    {
        _applicationDbContext = applicationDbContext;
        _roomService = roomService;
    }

    public async Task<Booking?> CreateNewBooking(BookingRequest request)
    {
        var availableRooms =
            _roomService.GetAvailableRooms(request.Occupants, request.CheckInDate, request.CheckOutDate);

        // Call customer service and validate customer here

        if (availableRooms.Any())
        {
            var booking = new Booking
            {
                CheckIn = request.CheckInDate,
                CheckOut = request.CheckOutDate,
                RoomId = availableRooms.FirstOrDefault().Id,
                CustomerId = request.CustomerId
            };

            _applicationDbContext.Bookings.Add(booking);
            await _applicationDbContext.SaveChangesAsync();
            return booking;
        }

        // we can create a custom response here
        return null;
    }

    public Booking? GetBooking(int bookId)
    {
        var booking = _applicationDbContext.Bookings.FirstOrDefault(x => x.Id == bookId);
        return booking;
    }
}