using System;
using System.Collections.Generic;
using HotelApp.DAL.Models;

namespace HotelApp.Tests;

public static class TestData
{
    public static Booking GetBooking(DateTime checkInDate, DateTime checkOutDate, int bookingId, int roomId, int customerId)
    {
        return new Booking
        {
            Id = bookingId,
            CheckIn = checkInDate,
            CheckOut = checkOutDate,
            RoomId = roomId,
            CustomerId = customerId,
        };
    }

    public static Room GetRoom(int id, RoomType roomType, int hotelId, List<Booking> bookings, int capacity)
    {
        return new Room
        {
            Id = id,
            RoomType = roomType,
            HotelId = hotelId,
            Bookings = bookings,
            Capacity = capacity
        };
    }
    
    public static List<Hotel> GetHotels()
    {
        return new List<Hotel>()
        {
            new Hotel
            {
                Id = 1,
                Name = "Hotel 1"
            },
            new Hotel
            {
                Id = 2,
                Name = "Hotel 2"
            }
        };
    }
    
}