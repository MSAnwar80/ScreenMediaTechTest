using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelApp.Controllers;
using HotelApp.DAL;
using HotelApp.DAL.Models;
using HotelApp.Models.Requests;
using HotelApp.Services;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace HotelApp.Tests;

public class BookingsTests
{
    [Test]
    public async Task CanCreateBooking()
    {
        var mockContext = new Mock<ApplicationDbContext>();

        var bookings = new List<Booking>()
        {
           new Booking
           {
               Id = 234,
               CheckIn = DateTime.Parse("Jan 1, 2009"),
               CheckOut = DateTime.Parse("Jan 3, 2009"),
               RoomId = 1,
               CustomerId = 1,
           }
        };
        
        var rooms = new List<Room>()
        {
            new Room
            {
                Id = 1,
                RoomType = RoomType.Double,
                HotelId = 1,
                Bookings = bookings,
                Capacity = 2
            }
        };
        
        mockContext.Setup(x => x.Rooms).ReturnsDbSet(rooms);
        mockContext.Setup(x => x.Bookings).ReturnsDbSet(bookings);
        var bookingService = new BookingService(mockContext.Object, new RoomService(mockContext.Object));
        var controller = new BookingsController(bookingService);
        var response = await controller.PostBooking(new BookingRequest
        {
            CheckInDate = DateTime.Parse("Jan 4, 2009"),
            CheckOutDate = DateTime.Parse("Jan 4, 2009"),
            CustomerId = 1,
            Occupants = 2
        });

        Assert.NotNull(response.Value);
    }
}