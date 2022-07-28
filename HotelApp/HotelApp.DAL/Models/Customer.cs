using System.ComponentModel.DataAnnotations;

namespace HotelApp.DAL.Models;

public class Customer
{
    public int Id { get; set; }

    [Required] public string Name { get; set; }

    public List<Booking> Bookings { get; set; }
}