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
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    
}