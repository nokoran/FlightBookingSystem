using FlightBookingSystem.Application.Interfaces;
using FlightBookingSystem.Application.DTOs;
using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Domain.Interfaces;

namespace FlightBookingSystem.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IFlightRepository _flightRepository;

    public BookingService(IBookingRepository bookingRepository, IFlightRepository flightRepository)
    {
        _bookingRepository = bookingRepository;
        _flightRepository = flightRepository;
    }

    public async Task BookSeatAsync(BookingDto dto, string userId)
    {
        // get flight with seats
        var flight = await _flightRepository.GetByIdAsync(dto.FlightId);

        if (flight == null)
            throw new Exception("Рейс не знайдено.");

        // logic for booking seat
        var seat = flight.Seats.FirstOrDefault(s => s.SeatNumber == dto.SeatNumber);
        // check if seat exists
        if (seat == null)
            throw new Exception($"Місце {dto.SeatNumber} не знайдено на цьому рейсі.");

        // check if seat is already booked
        if (seat.IsBooked)
            throw new Exception("Це місце вже зайняте.");
        
        // create booking
        seat.IsBooked = true;

        var booking = new Booking
        {
            UserId = userId,
            FlightId = flight.Id,
            SeatId = seat.Id,
            BookingDate = DateTime.UtcNow,
            IsCancelled = false
        };

        await _bookingRepository.AddAsync(booking);
        await _bookingRepository.SaveChangesAsync();
    }
    
    public async Task CancelBookingAsync(int bookingId, string userId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);

        if (booking == null)
            throw new KeyNotFoundException("Бронювання не знайдено.");

        if (booking.UserId != userId)
            throw new UnauthorizedAccessException("Ви не можете скасувати чуже бронювання.");

        if (booking.IsCancelled)
            throw new Exception("Бронювання вже скасовано.");

        // Soft delete
        booking.IsCancelled = true;

        // Free up the seat
        if (booking.Seat != null)
        {
            booking.Seat.IsBooked = false;
        }

        await _bookingRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<object>> GetMyBookingsAsync(string userId)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId);
        
        return bookings.Select(b => new
        {
            BookingId = b.Id,
            Flight = b.Flight.FlightNumber,
            Route = $"{b.Flight.DepartureCity} -> {b.Flight.ArrivalCity}",
            Seat = b.Seat.SeatNumber,
            Date = b.BookingDate,
            IsCancelled = b.IsCancelled
        });
    }
}