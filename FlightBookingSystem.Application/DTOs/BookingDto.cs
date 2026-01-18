using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystem.Application.DTOs;

public class BookingDto
{
    [Required]
    public int FlightId { get; set; }
    [Required]
    public string SeatNumber { get; set; } // e.g., "12A"
}