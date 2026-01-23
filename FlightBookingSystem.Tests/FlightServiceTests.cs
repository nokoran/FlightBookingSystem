using FlightBookingSystem.Application.DTOs;
using FlightBookingSystem.Application.Services;
using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Domain.Interfaces;
using Moq;
using Xunit;

namespace FlightBookingSystem.Tests;

public class FlightServiceTests
{
    private readonly Mock<IFlightRepository> _flightRepoMock;
    private readonly Mock<IBookingRepository> _bookingRepoMock;
    private readonly FlightService _flightService;

    public FlightServiceTests()
    {
        _flightRepoMock = new Mock<IFlightRepository>();
        _bookingRepoMock = new Mock<IBookingRepository>();
        _flightService = new FlightService(_flightRepoMock.Object, _bookingRepoMock.Object);
    }

    [Fact]
    public async Task CreateFlight_ShouldGenerateCorrectNumberOfSeats()
    {
        // Arrange
        var flightId = 777;
        var flight = new Flight 
        { 
            Id = flightId, 
            Rows = 2, 
            SeatsPerRow = 2 
        };
        
        var existingBookings = new List<Booking>
        {
            new Booking { RowId = 1, LetterId = 1, IsCancelled = false},
            new Booking { RowId = 2, LetterId = 1, IsCancelled = false }
        };
        
        _flightRepoMock.Setup(r => r.GetByIdAsync(flightId))
            .ReturnsAsync(flight);

        _bookingRepoMock.Setup(r => r.GetBookingsByFlightIdAsync(flightId))
            .ReturnsAsync(existingBookings);

        // Act
        var result = await _flightService.GetSeatsByFlightIdAsync(flightId);

        // Assert
        Assert.Equal(4, result.Count());
        Assert.Contains(result, s => s is { SeatNumber: "1A", IsBooked: true });
        Assert.Contains(result, s => s is { SeatNumber: "1B", IsBooked: false });
        Assert.Contains(result, s => s is { SeatNumber: "2A", IsBooked: true });
        Assert.Contains(result, s => s is { SeatNumber: "2B", IsBooked: false });
    }
}