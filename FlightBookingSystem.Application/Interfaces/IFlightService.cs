using FlightBookingSystem.Application.DTOs;

namespace FlightBookingSystem.Application.Interfaces;

public interface IFlightService
{
    Task CreateFlightAsync(CreateFlightDto dto);
    Task<IEnumerable<FlightDto>> GetAllFlightsAsync();
    Task<FlightDto?> GetFlightByIdAsync(int id);
    Task<IEnumerable<SeatDto>> GetSeatsByFlightIdAsync(int flightId);
    Task UpdateFlightAsync(int id, UpdateFlightDto dto);
    Task DeleteFlightAsync(int id);
}