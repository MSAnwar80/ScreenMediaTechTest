using System.ComponentModel.DataAnnotations;

namespace HotelApp.DAL.Models;

public class Hotel
{
    public int Id { get; set; }

    [Required] public string Name { get; set; }

    public List<Room> Rooms { get; set; }
}