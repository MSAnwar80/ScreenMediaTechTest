using HotelApp.DAL.Models;

namespace HotelApp.Models.Requests;

public class BookingRequest
{
    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public int CustomerId { get; set; }

    public int Occupants { get; set; }
}