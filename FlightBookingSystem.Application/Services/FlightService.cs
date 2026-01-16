using FlightBookingSystem.Application.DTOs;
using FlightBookingSystem.Application.Interfaces;
using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Domain.Interfaces;

namespace FlightBookingSystem.Application.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _repository;

    public FlightService(IFlightRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateFlightAsync(CreateFlightDto dto)
    {
        // mapping DTO to Entity
        var flight = new Flight
        {
            FlightNumber = dto.FlightNumber,
            DepartureCity = dto.DepartureCity,
            ArrivalCity = dto.ArrivalCity,
            DepartureTime = dto.DepartureTime,
            ArrivalTime = dto.ArrivalTime
        };

        // generate seats
        string seatLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        if (dto.SeatsPerRow > seatLetters.Length)
        {
            throw new ArgumentException($"Максимальна кількість місць у ряду: {seatLetters.Length}");
        }
            
        for (int r = 1; r <= dto.Rows; r++)
        {
            for (int s = 0; s < dto.SeatsPerRow; s++)
            {
                flight.Seats.Add(new Seat
                {
                    SeatNumber = $"{r}{seatLetters[s]}",
                    IsBooked = false
                });
            }
        }

        await _repository.AddAsync(flight);
        await _repository.SaveChangesAsync();
    }

    public async Task<IEnumerable<FlightDto>> GetAllFlightsAsync()
    {
        var flights = await _repository.GetAllAsync();
            
        // mapping Entities to DTO
        return flights.Select(f => new FlightDto
        {
            Id = f.Id,
            FlightNumber = f.FlightNumber,
            DepartureCity = f.DepartureCity,
            ArrivalCity = f.ArrivalCity,
            DepartureTime = f.DepartureTime,
            ArrivalTime = f.ArrivalTime,
            TotalSeats = f.Seats.Count
        });
    }
    
    public async Task<FlightDto?> GetFlightByIdAsync(int id)
    {
        // mapping Entity to DTO
        var f = await _repository.GetByIdAsync(id);
        if (f == null) return null;

        return new FlightDto
        {
            Id = f.Id,
            FlightNumber = f.FlightNumber,
            DepartureCity = f.DepartureCity,
            ArrivalCity = f.ArrivalCity,
            DepartureTime = f.DepartureTime,
            ArrivalTime = f.ArrivalTime,
            TotalSeats = f.Seats.Count
        };
    }

    public async Task UpdateFlightAsync(int id, CreateFlightDto dto)
    {
        var flight = await _repository.GetByIdAsync(id);
        if (flight == null) throw new KeyNotFoundException("Рейс не знайдено");

        // updating fields
        flight.FlightNumber = dto.FlightNumber;
        flight.DepartureCity = dto.DepartureCity;
        flight.ArrivalCity = dto.ArrivalCity;
        flight.DepartureTime = dto.DepartureTime;
        flight.ArrivalTime = dto.ArrivalTime;

        await _repository.SaveChangesAsync();
    }

    public async Task DeleteFlightAsync(int id)
    {
        var flight = await _repository.GetByIdAsync(id);
        if (flight == null) throw new KeyNotFoundException("Рейс не знайдено");

        await _repository.DeleteAsync(flight);
        await _repository.SaveChangesAsync();
    }
}