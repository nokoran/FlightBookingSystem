using FlightBookingSystem.Application.DTOs;
using FlightBookingSystem.Application.Interfaces;
using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Domain.Interfaces;

namespace FlightBookingSystem.Application.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IBookingRepository _bookingRepository;

    public FlightService(IFlightRepository flightRepository, IBookingRepository bookingRepository)
    {
        _flightRepository = flightRepository;
        _bookingRepository = bookingRepository;
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
            ArrivalTime = dto.ArrivalTime,
            Rows = dto.Rows,
            SeatsPerRow = dto.SeatsPerRow,
        };
        
        await _flightRepository.AddAsync(flight);
        await _flightRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<FlightDto>> GetAllFlightsAsync()
    {
        var flights = await _flightRepository.GetAllAsync();
            
        // mapping Entities to DTO
        return flights.Select(f => new FlightDto
        {
            Id = f.Id,
            FlightNumber = f.FlightNumber,
            DepartureCity = f.DepartureCity,
            ArrivalCity = f.ArrivalCity,
            DepartureTime = f.DepartureTime,
            ArrivalTime = f.ArrivalTime,
            Rows = f.Rows,
            SeatsPerRow = f.SeatsPerRow
        });
    }
    
    public async Task<FlightDto?> GetFlightByIdAsync(int id)
    {
        // mapping Entity to DTO
        var f = await _flightRepository.GetByIdAsync(id);
        if (f == null) return null;

        return new FlightDto
        {
            Id = f.Id,
            FlightNumber = f.FlightNumber,
            DepartureCity = f.DepartureCity,
            ArrivalCity = f.ArrivalCity,
            DepartureTime = f.DepartureTime,
            ArrivalTime = f.ArrivalTime,
            Rows = f.Rows,
            SeatsPerRow = f.SeatsPerRow
        };
    }

    public async Task<IEnumerable<SeatDto>> GetSeatsByFlightIdAsync(int flightId)
    {
        var flight = await _flightRepository.GetByIdAsync(flightId);

        if (flight == null)
            throw new KeyNotFoundException("Рейс не знайдено.");
        
        var bookings = await _bookingRepository.GetBookingsByFlightIdAsync(flightId);
        
        var bookedSeats = bookings.Select(b => (b.RowId, b.LetterId)).ToHashSet();
        var seats = new List<SeatDto>();
        
        for (int row = 1; row <= flight.Rows; row++)
        {
            for (int letter = 1; letter <= flight.SeatsPerRow; letter++)
            {
                seats.Add(new SeatDto
                {
                    SeatNumber = $"{row}{(char)('A' + letter - 1)}",
                    IsBooked = bookedSeats.Contains((row, letter))
                });
            }
        }
        return seats;
    }
    
    public async Task UpdateFlightAsync(int id, UpdateFlightDto dto)
    {
        var flight = await _flightRepository.GetByIdAsync(id);
        if (flight == null) throw new KeyNotFoundException("Рейс не знайдено");

        // updating fields
        flight.FlightNumber = dto.FlightNumber;
        flight.DepartureCity = dto.DepartureCity;
        flight.ArrivalCity = dto.ArrivalCity;
        flight.DepartureTime = dto.DepartureTime;
        flight.ArrivalTime = dto.ArrivalTime;
        flight.Rows = dto.Rows;
        flight.SeatsPerRow = dto.SeatsPerRow;

        await _flightRepository.SaveChangesAsync();
    }

    public async Task DeleteFlightAsync(int id)
    {
        var flight = await _flightRepository.GetByIdAsync(id);
        if (flight == null) throw new KeyNotFoundException("Рейс не знайдено");

        await _flightRepository.DeleteAsync(flight);
        await _flightRepository.SaveChangesAsync();
    }
}