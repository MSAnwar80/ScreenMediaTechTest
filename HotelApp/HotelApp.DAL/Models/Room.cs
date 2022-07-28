using System.ComponentModel.DataAnnotations;

namespace HotelApp.DAL.Models;

public class Room
{
    public int Id { get; set; }
    
    [Required]
    public int Capacity { get; set; }

    [Required] // RoomType needs more thought - Maybe an RoomType entity would be better, where we can define capacity etc. 
    public RoomType RoomType { get; set; }
    
    public int HotelId { get; set; }

    public Hotel Hotel { get; set; }
    
    public List<Booking> Bookings { get; set; }
}