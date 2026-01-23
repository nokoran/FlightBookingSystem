namespace FlightBookingSystem.Domain.Interfaces;
using FlightBookingSystem.Domain.Entities;

public interface IBookingRepository
{
    Task AddAsync(Booking booking);
    Task<IEnumerable<Booking>> GetByUserIdAsync(string userId);
    Task<Booking?> GetByIdAsync(int id);
    Task<bool> BookingExistsAsync(int flightId, int rowId, int letterId);
    Task<IEnumerable<Booking>> GetBookingsByFlightIdAsync(int flightId);
    Task SaveChangesAsync();
}