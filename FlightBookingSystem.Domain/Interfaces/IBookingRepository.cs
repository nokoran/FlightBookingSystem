namespace FlightBookingSystem.Domain.Interfaces;
using FlightBookingSystem.Domain.Entities;

public interface IBookingRepository
{
    Task AddAsync(Booking booking);
    Task<IEnumerable<Booking>> GetByUserIdAsync(string userId);
    Task<Booking?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}