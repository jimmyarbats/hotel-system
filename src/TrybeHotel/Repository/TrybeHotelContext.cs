using Microsoft.EntityFrameworkCore;
using TrybeHotel.Models;

namespace TrybeHotel.Repository;
public class TrybeHotelContext : DbContext, ITrybeHotelContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users{ get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public TrybeHotelContext(DbContextOptions<TrybeHotelContext> options) : base(options) { }
    public TrybeHotelContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
{
    modelBuilder.Entity<Hotel>()
        .HasOne(hotel => hotel.City)
        .WithMany(city => city.Hotels)
        .HasForeignKey(hotel => hotel.CityId);

    modelBuilder.Entity<Room>()
        .HasOne(room => room.Hotel)
        .WithMany(hotel => hotel.Rooms)
        .HasForeignKey(room => room.HotelId);
}

}