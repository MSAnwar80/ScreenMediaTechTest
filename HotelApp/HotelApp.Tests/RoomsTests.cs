using System;
using System.Collections.Generic;
using HotelApp.Controllers;
using HotelApp.DAL;
using HotelApp.DAL.Models;
using HotelApp.Services;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace HotelApp.Tests;

public class RoomsTests
{
    [Test]
    public void ValidRequestReturnsAvailableRooms()
    {
        var mockContext = new Mock<ApplicationDbContext>();

        var bookings = new List<Booking>()
        {
            TestData.GetBooking(DateTime.Parse("Jan 1, 2009"), DateTime.Parse("Jan 3, 2009"),234, 1,1)
        };
        
        var rooms = new List<Room>()
        {
            TestData.GetRoom(1,RoomType.Double, 1,bookings, 2)
        };
        
        mockContext.Setup(x => x.Rooms).ReturnsDbSet(rooms);
        mockContext.Setup(x => x.Bookings).ReturnsDbSet(bookings);

        var roomsService = new RoomService(mockContext.Object);
        var controller = new RoomsController(roomsService);

        var response = controller.GetAvailableRooms(2, DateTime.Now, DateTime.Now);
        
        Assert.NotNull(response.Value);
        Assert.AreEqual(response.Value.Count, 1);
    }
    
    [Test]
    public void RoomWithHigherCapacityIsReturned()
    {
        var mockContext = new Mock<ApplicationDbContext>();

        var bookings = new List<Booking>()
        {
            TestData.GetBooking(DateTime.Parse("Jan 1, 2009"), DateTime.Parse("Jan 3, 2009"),234, 1,1)
        };
        
        var rooms = new List<Room>()
        {
            TestData.GetRoom(1,RoomType.Double, 1,bookings, 2),
            TestData.GetRoom(2,RoomType.Deluxe, 1,bookings, 4),
            TestData.GetRoom(2,RoomType.Single, 1,bookings, 1)
        };
        
        mockContext.Setup(x => x.Rooms).ReturnsDbSet(rooms);
        mockContext.Setup(x => x.Bookings).ReturnsDbSet(bookings);

        var roomsService = new RoomService(mockContext.Object);
        var controller = new RoomsController(roomsService);

        var response = controller.GetAvailableRooms(2, DateTime.Now, DateTime.Now);
        
        Assert.NotNull(response.Value);
        Assert.AreEqual(2,response.Value.Count);
    }
    
    [Test]
    public void NoRoomWithRequiredCapacityFound()
    {
        var mockContext = new Mock<ApplicationDbContext>();

        var rooms = new List<Room>()
        {
            TestData.GetRoom(1,RoomType.Single, 1,null, 1)
        };
        
        mockContext.Setup(x => x.Rooms).ReturnsDbSet(rooms);
     
        var roomsService = new RoomService(mockContext.Object);
        var controller = new RoomsController(roomsService);

        var response = controller.GetAvailableRooms(2, DateTime.Now, DateTime.Now);
        
        Assert.AreEqual(response.Value.Count, 0);
    }
    
    [Test]
    public void RoomIsNotReturnedWhenBookingOverlapsRequestedDates()
    {
        var mockContext = new Mock<ApplicationDbContext>();

        var bookings = new List<Booking>()
        {
            TestData.GetBooking(DateTime.Parse("Jan 1, 2009"), DateTime.Parse("Jan 3, 2009"),234, 1,1)
        };
        
        var rooms = new List<Room>()
        {
            TestData.GetRoom(1,RoomType.Double, 1,bookings, 2)
        };
        
        mockContext.Setup(x => x.Rooms).ReturnsDbSet(rooms);
        mockContext.Setup(x => x.Bookings).ReturnsDbSet(bookings);

        var roomsService = new RoomService(mockContext.Object);
        var controller = new RoomsController(roomsService);

        var response = controller.GetAvailableRooms(2, DateTime.Parse("Jan 3, 2009"), DateTime.Parse("Jan 4, 2009"));
        
        Assert.NotNull(response.Value);
        Assert.AreEqual(response.Value.Count, 0);
    }
    
}