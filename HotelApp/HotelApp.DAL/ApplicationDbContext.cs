using HotelApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.DAL;

public class ApplicationDbContext : DbContext
{
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
     {
     }
     
     public ApplicationDbContext()
     {
     }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }
}