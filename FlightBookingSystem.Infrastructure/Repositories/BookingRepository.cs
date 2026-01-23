using FlightBookingSystem.Domain.Interfaces;
using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace FlightBookingSystem.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly FlightDbContext _context;

    public BookingRepository(FlightDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
    }

    public async Task<IEnumerable<Booking>> GetByUserIdAsync(string userId)
    {
        return await _context.Bookings
            .Where(b => b.UserId == userId)
            .Include(b => b.Flight)
            .Include(b => b.Row)
            .Include(b => b.Letter)
            .ToListAsync();
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        return await _context.Bookings
            .Include(b => b.Row)
            .Include(b => b.Letter)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
    
    public async Task<bool> BookingExistsAsync(int flightId, int rowId, int letterId)
    {
        return await _context.Bookings
            .AnyAsync(b => b.FlightId == flightId && b.RowId == rowId && b.LetterId == letterId && !b.IsCancelled);
    }
    
    public async Task<IEnumerable<Booking>> GetBookingsByFlightIdAsync(int flightId)
    {
        return await _context.Bookings
            .Where(b => b.FlightId == flightId && !b.IsCancelled)
            .Include(b => b.Row)
            .Include(b => b.Letter)
            .Include(b => b.User)
            .ToListAsync();
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}