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
    public DbSet<SeatLetter> SeatLetters { get; set; }
    public DbSet<Row> Rows { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Flight>(entity =>
        {
            entity.Property(f => f.FlightNumber)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(f => f.DepartureCity)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(f => f.ArrivalCity)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(f => f.Rows)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(f => f.SeatsPerRow)
                .IsRequired()
                .HasMaxLength(26);
        });
        
        // Booking -> Seat
        builder.Entity<Booking>()
            .HasOne(b => b.Row)
            .WithMany()
            .HasForeignKey(b => b.RowId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<Booking>()
            .HasOne(b => b.Letter)
            .WithMany()
            .HasForeignKey(b => b.LetterId)
            .OnDelete(DeleteBehavior.Restrict);

        // Booking -> Flight
        builder.Entity<Booking>()
            .HasOne(b => b.Flight)
            .WithMany()
            .HasForeignKey(b => b.FlightId)
            .OnDelete(DeleteBehavior.Restrict);

        // Booking -> User
        builder.Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId);
        
        
        builder.Entity<SeatLetter>()
            .HasData(
                new SeatLetter { Id = 1, Letter = 'A' },
                new SeatLetter { Id = 2, Letter = 'B' },
                new SeatLetter { Id = 3, Letter = 'C' },
                new SeatLetter { Id = 4, Letter = 'D' },
                new SeatLetter { Id = 5, Letter = 'E' },
                new SeatLetter { Id = 6, Letter = 'F' },
                new SeatLetter { Id = 7, Letter = 'G' },
                new SeatLetter { Id = 8, Letter = 'H' },
                new SeatLetter { Id = 9, Letter = 'I' },
                new SeatLetter { Id = 10, Letter = 'J' },
                new SeatLetter { Id = 11, Letter = 'K' },
                new SeatLetter { Id = 12, Letter = 'L' },
                new SeatLetter { Id = 13, Letter = 'M' },
                new SeatLetter { Id = 14, Letter = 'N' },
                new SeatLetter { Id = 15, Letter = 'O' },
                new SeatLetter { Id = 16, Letter = 'P' },
                new SeatLetter { Id = 17, Letter = 'Q' },
                new SeatLetter { Id = 18, Letter = 'R' },
                new SeatLetter { Id = 19, Letter = 'S' },
                new SeatLetter { Id = 20, Letter = 'T' },
                new SeatLetter { Id = 21, Letter = 'U' },
                new SeatLetter { Id = 22, Letter = 'V' },
                new SeatLetter { Id = 23, Letter = 'W' },
                new SeatLetter { Id = 24, Letter = 'X' },
                new SeatLetter { Id = 25, Letter = 'Y' },
                new SeatLetter { Id = 26, Letter = 'Z' }
            );
        builder.Entity<SeatLetter>()
            .HasIndex(sl => sl.Letter)
            .IsUnique();

        builder.Entity<Row>()
            .HasData(
                new Row { Id = 1, RowNumber = 1 },
                new Row { Id = 2, RowNumber = 2 },
                new Row { Id = 3, RowNumber = 3 },
                new Row { Id = 4, RowNumber = 4 },
                new Row { Id = 5, RowNumber = 5 },
                new Row { Id = 6, RowNumber = 6 },
                new Row { Id = 7, RowNumber = 7 },
                new Row { Id = 8, RowNumber = 8 },
                new Row { Id = 9, RowNumber = 9 },
                new Row { Id = 10, RowNumber = 10 },
                new Row { Id = 11, RowNumber = 11 },
                new Row { Id = 12, RowNumber = 12 },
                new Row { Id = 13, RowNumber = 13 },
                new Row { Id = 14, RowNumber = 14 },
                new Row { Id = 15, RowNumber = 15 },
                new Row { Id = 16, RowNumber = 16 },
                new Row { Id = 17, RowNumber = 17 },
                new Row { Id = 18, RowNumber = 18 },
                new Row { Id = 19, RowNumber = 19 },
                new Row { Id = 20, RowNumber = 20 },
                new Row { Id = 21, RowNumber = 21 },
                new Row { Id = 22, RowNumber = 22 },
                new Row { Id = 23, RowNumber = 23 },
                new Row { Id = 24, RowNumber = 24 },
                new Row { Id = 25, RowNumber = 25 },
                new Row { Id = 26, RowNumber = 26 },
                new Row { Id = 27, RowNumber = 27 },
                new Row { Id = 28, RowNumber = 28 },
                new Row { Id = 29, RowNumber = 29 },
                new Row { Id = 30, RowNumber = 30 },
                new Row { Id = 31, RowNumber = 31 },
                new Row { Id = 32, RowNumber = 32 },
                new Row { Id = 33, RowNumber = 33 },
                new Row { Id = 34, RowNumber = 34 },
                new Row { Id = 35, RowNumber = 35 },
                new Row { Id = 36, RowNumber = 36 },
                new Row { Id = 37, RowNumber = 37 },
                new Row { Id = 38, RowNumber = 38 },
                new Row { Id = 39, RowNumber = 39 },
                new Row { Id = 40, RowNumber = 40 },
                new Row { Id = 41, RowNumber = 41 },
                new Row { Id = 42, RowNumber = 42 },
                new Row { Id = 43, RowNumber = 43 },
                new Row { Id = 44, RowNumber = 44 },
                new Row { Id = 45, RowNumber = 45 },
                new Row { Id = 46, RowNumber = 46 },
                new Row { Id = 47, RowNumber = 47 },
                new Row { Id = 48, RowNumber = 48 },
                new Row { Id = 49, RowNumber = 49 },
                new Row { Id = 50, RowNumber = 50 }
            );

    }
    
}