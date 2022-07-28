using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelApp.Controllers;
using HotelApp.DAL;
using HotelApp.DAL.Models;
using HotelApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace HotelApp.Tests;

public class HotelTests
{
    [Test]
    public async Task CanGetHotelByName()
    {
        var mockContext = new Mock<ApplicationDbContext>();

        var hotels = TestData.GetHotels();
        mockContext.Setup(x => x.Hotels).ReturnsDbSet(hotels);
        var hotelService = new HotelService(mockContext.Object);
        var controller = new HotelsController(hotelService);
        var response = await controller.GetHotel("Hotel 1");

        Assert.NotNull(response.Value);
        Assert.IsTrue(response.Value?.Id == 1);
    }

    [Test]
    public async Task CanGetHotelByName_NotFoundIsReturned()
    {
        var mockContext = new Mock<ApplicationDbContext>();

        var hotels = TestData.GetHotels();
        mockContext.Setup(x => x.Hotels).ReturnsDbSet(hotels);

        var mockHotelService = new Mock<HotelService>(mockContext.Object);

        var controller = new HotelsController(mockHotelService.Object);
        var response = await controller.GetHotel("Hotel 11");

        Assert.IsNull(response.Value);
    }
}