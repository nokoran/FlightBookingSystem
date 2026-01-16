using FlightBookingSystem.Domain.Entities;

namespace FlightBookingSystem.Domain.Interfaces;

public interface IFlightRepository
{
    Task AddAsync(Flight flight);
    Task<IEnumerable<Flight>> GetAllAsync();
    Task<Flight?> GetByIdAsync(int id);
    Task DeleteAsync(Flight flight);
    Task SaveChangesAsync();
}