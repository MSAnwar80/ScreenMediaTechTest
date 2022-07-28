using HotelApp.DAL.Models;
using HotelApp.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Services;

public interface IBookingService
{
    Task<Booking?> CreateNewBooking(BookingRequest request);

    Booking? GetBooking(int bookId);
}