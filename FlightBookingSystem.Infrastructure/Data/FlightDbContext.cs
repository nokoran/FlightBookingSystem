using FlightBookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingSystem.Infrastructure.Data;

public class FlightDbContext : IdentityDbContext<User>
{
    public FlightDbContext(DbContextOptions<FlightDbContext> options) : base(options)
    {
    }
    
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Booking -> Seat
        builder.Entity<Booking>()
            .HasOne(b => b.Seat)
            .WithMany()
            .HasForeignKey(b => b.SeatId)
            .OnDelete(DeleteBehavior.Restrict); // forbid cascade delete

        // Booking -> Flight
        builder.Entity<Booking>()
            .HasOne(b => b.Flight)
            .WithMany()
            .HasForeignKey(b => b.FlightId)
            .OnDelete(DeleteBehavior.Restrict); // forbid cascade delete
    }
    
}