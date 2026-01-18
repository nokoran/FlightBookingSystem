using FlightBookingSystem.Application.DTOs;
using FlightBookingSystem.Application.Services;
using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Domain.Interfaces;
using Moq;
using Xunit;

namespace FlightBookingSystem.Tests;

public class BookingServiceTests
{
    private readonly Mock<IBookingRepository> _bookingRepoMock;
    private readonly Mock<IFlightRepository> _flightRepoMock;
    private readonly BookingService _bookingService;

    public BookingServiceTests()
    {
        _bookingRepoMock = new Mock<IBookingRepository>();
        _flightRepoMock = new Mock<IFlightRepository>();
        
        _bookingService = new BookingService(_bookingRepoMock.Object, _flightRepoMock.Object);
    }

    [Fact]
    public async Task BookSeat_ShouldThrowException_WhenSeatIsAlreadyBooked()
    {
        // Arrange
        var flightId = 1;
        var seatNumber = "1A";
        var userId = "user-123";

        // create a flight with the seat already booked
        var flight = new Flight
        {
            Id = flightId,
            Seats = new List<Seat>
            {
                new Seat { Id = 100, SeatNumber = "1A", IsBooked = true }
            }
        };

        // return the flight when requested
        _flightRepoMock.Setup(repo => repo.GetByIdAsync(flightId))
            .ReturnsAsync(flight);

        var bookingDto = new BookingDto { FlightId = flightId, SeatNumber = seatNumber };

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            _bookingService.BookSeatAsync(bookingDto, userId));

        // Assert
        Assert.Equal("Це місце вже зайняте.", exception.Message);
        
        // determine that AddAsync was never called
        _bookingRepoMock.Verify(repo => repo.AddAsync(It.IsAny<Booking>()), Times.Never);
    }

    [Fact]
    public async Task BookSeat_ShouldSucceed_WhenSeatIsFree()
    {
        // 1. Arrange
        var flightId = 1;
        var userId = "user-123";

        // create a flight with the seat free
        var flight = new Flight
        {
            Id = flightId,
            Seats = new List<Seat>
            {
                new Seat { Id = 100, SeatNumber = "1A", IsBooked = false }
            }
        };

        
        // return the flight when requested
        _flightRepoMock.Setup(repo => repo.GetByIdAsync(flightId)).ReturnsAsync(flight);

        var bookingDto = new BookingDto { FlightId = flightId, SeatNumber = "1A" };

        // 2. Act
        await _bookingService.BookSeatAsync(bookingDto, userId);

        // 3. Assert
        // determine that AddAsync was called once
        _bookingRepoMock.Verify(repo => repo.AddAsync(It.IsAny<Booking>()), Times.Once);
        
        Assert.True(flight.Seats.First().IsBooked);
    }
}