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

    public async Task BookSeatAsync(CreateBookingDto dto, string userId)
    {
        // get flight with seats
        var flight = await _flightRepository.GetByIdAsync(dto.FlightId);

        if (flight == null)
            throw new KeyNotFoundException("Рейс не знайдено.");

        // logic for booking seat
        if (dto.RowId > flight.Rows || dto.RowId < 1)
            throw new ArgumentException("Неправильний номер ряду.");
        if (dto.LetterId > flight.SeatsPerRow || dto.LetterId < 1)
            throw new ArgumentException("Неправильна літера місця.");
        
        
        // check if seat is already booked
        bool isBooked = await _bookingRepository.BookingExistsAsync(dto.FlightId, dto.RowId, dto.LetterId);
        if (isBooked)
            throw new InvalidOperationException("Це місце вже зайняте.");
        
        
        var booking = new Booking
        {
            UserId = userId,
            FlightId = flight.Id,
            RowId = dto.RowId,
            LetterId = dto.LetterId,
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
            throw new InvalidOperationException("Бронювання вже скасовано.");
        
        booking.IsCancelled = true;

        await _bookingRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<BookingDto>> GetMyBookingsAsync(string userId)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId);
        
        return bookings.Select(b => new BookingDto
        {
            BookingId = b.Id,
            FlightNumber = b.Flight.FlightNumber,
            DepartureCity = b.Flight.DepartureCity,
            ArrivalCity = b.Flight.ArrivalCity,
            Seat = $"{b.Row.RowNumber}{b.Letter.Letter}",
            Date = b.BookingDate,
            IsCancelled = b.IsCancelled
        });
    }
}