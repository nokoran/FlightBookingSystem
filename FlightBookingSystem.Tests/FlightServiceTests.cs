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
    private readonly FlightService _flightService;

    public FlightServiceTests()
    {
        _flightRepoMock = new Mock<IFlightRepository>();
        _flightService = new FlightService(_flightRepoMock.Object);
    }

    [Fact]
    public async Task CreateFlight_ShouldGenerateCorrectNumberOfSeats()
    {
        // Arrange
        var dto = new CreateFlightDto
        {
            FlightNumber = "TEST100",
            DepartureCity = "Kyiv",
            ArrivalCity = "Lviv",
            DepartureTime = DateTime.Now.AddDays(1),
            ArrivalTime = DateTime.Now.AddDays(1).AddHours(2),
            Rows = 10, // 10 rows
            SeatsPerRow = 6 // 6 seats per row (A-F)
        };

        // Act
        await _flightService.CreateFlightAsync(dto);

        // Assert
        _flightRepoMock.Verify(repo => repo.AddAsync(It.Is<Flight>(f =>
                f.FlightNumber == "TEST100" &&
                f.Seats.Count == 60 // 10 * 6 = 60
        )), Times.Once);
    }
}