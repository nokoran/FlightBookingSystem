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
        var flightId = 100;
        var rowId = 5;
        var letterId = 1;
        var userId = "user-test";
        
        var flight = new Flight 
        { 
            Id = flightId, 
            Rows = 20, 
            SeatsPerRow = 6 
        };
        
        _flightRepoMock.Setup(repo => repo.GetByIdAsync(flightId))
            .ReturnsAsync(flight);
        
        _bookingRepoMock.Setup(repo => repo.BookingExistsAsync(flightId, rowId, letterId))
            .ReturnsAsync(true);

        var bookingDto = new CreateBookingDto 
        { 
            FlightId = flightId, 
            RowId = rowId, 
            LetterId = letterId 
        };

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => 
            _bookingService.BookSeatAsync(bookingDto, userId));

        // Assert
        Assert.Equal("Це місце вже зайняте.", exception.Message);

        // Verify
        _bookingRepoMock.Verify(repo => repo.AddAsync(It.IsAny<Booking>()), Times.Never);
    }

    [Fact]
    public async Task BookSeat_ShouldSucceed_WhenSeatIsFree()
    {
        // Arrange
        var flightId = 100;
        var rowId = 5;
        var letterId = 1;
        var userId = "user-123";
        
        var flight = new Flight 
        { 
            Id = flightId, 
            Rows = 20, 
            SeatsPerRow = 6 
        };
        
        _flightRepoMock.Setup(repo => repo.GetByIdAsync(flightId))
            .ReturnsAsync(flight);
        
        _bookingRepoMock.Setup(repo => repo.BookingExistsAsync(flightId, rowId, letterId))
            .ReturnsAsync(false);

        var bookingDto = new CreateBookingDto 
        { 
            FlightId = flightId, 
            RowId = rowId, 
            LetterId = letterId 
        };

        // Act
        await _bookingService.BookSeatAsync(bookingDto, userId);

        //Assert
        _bookingRepoMock.Verify(repo => repo.AddAsync(It.Is<Booking>(b => 
            b.FlightId == flightId &&
            b.RowId == rowId &&
            b.LetterId == letterId &&
            b.UserId == userId &&
            b.IsCancelled == false
        )), Times.Once);
    }
}