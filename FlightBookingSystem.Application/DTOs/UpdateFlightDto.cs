using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystem.Application.DTOs;

public class UpdateFlightDto
{
    [Required]
    public string FlightNumber { get; set; }
    [Required]
    public string DepartureCity { get; set; }
    [Required]
    public string ArrivalCity { get; set; }
    [Required]
    public DateTime DepartureTime { get; set; }
    [Required]
    public DateTime ArrivalTime { get; set; }

    // additional properties for seat configuration
    [Range(1, 50)]
    public int Rows { get; set; } = 20; // quantity of rows
    [Range(1, 10)]
    public int SeatsPerRow { get; set; } = 6; // quantity of seats per row
}