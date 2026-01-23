using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Domain.Interfaces;
using FlightBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingSystem.Infrastructure.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly FlightDbContext _context;

    public FlightRepository(FlightDbContext context)
    {
        _context = context;
    }

    public async Task<Flight?> GetByIdAsync(int id)
    {
        // flight with seats
        return await _context.Flights
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public Task DeleteAsync(Flight flight)
    {
        _context.Flights.Remove(flight);
        return Task.CompletedTask;
    }
    
    public async Task AddAsync(Flight flight)
    {
        await _context.Flights.AddAsync(flight);
    }

    public async Task<IEnumerable<Flight>> GetAllAsync()
    {
        return await _context.Flights.ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}