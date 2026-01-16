using FlightBookingSystem.Application.DTOs;

namespace FlightBookingSystem.Application.Interfaces;

public interface IFlightService
{
    Task CreateFlightAsync(CreateFlightDto dto);
    Task<IEnumerable<FlightDto>> GetAllFlightsAsync();
    Task<FlightDto?> GetFlightByIdAsync(int id);
    Task UpdateFlightAsync(int id, CreateFlightDto dto);
    Task DeleteFlightAsync(int id);
}