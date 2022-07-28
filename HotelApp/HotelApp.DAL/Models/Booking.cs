using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelApp.DAL.Models;

public class Booking
{
    public int Id { get; set; }

    [Required] public DateTime CheckIn { get; set; }

    [Required] public DateTime CheckOut { get; set; }

    public DateTime BookingDate => DateTime.Now;

    public int RoomId { get; set; }

    [JsonIgnore] public Room Room { get; set; }

    public int CustomerId { get; set; }

    [JsonIgnore] public Customer Customer { get; set; }
}